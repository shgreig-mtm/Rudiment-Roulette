import { ref, onUnmounted } from 'vue';

interface Stroke {
  hand: 'R' | 'L';
  isGrace: boolean;
  duration: number; // Relative duration based on subdivision
}

export function useDrumAudio() {
  const audioContext = ref<AudioContext | null>(null);
  const isPlaying = ref(false);
  const snareBuffer = ref<AudioBuffer | null>(null);
  const currentHighlightIndex = ref(-1); // Track the currently playing stroke for UI highlighting
  let schedulerInterval: number | null = null;
  let nextNoteTime = 0;
  let currentStrokeIndex = 0;
  let pattern: Stroke[] = [];
  let beatDuration = 0;

  // Initialize audio context and load snare sample
  const initAudio = async () => {
    if (!audioContext.value) {
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      audioContext.value = new (window.AudioContext || (window as any).webkitAudioContext)();

      // Load the snare sample
      try {
        const response = await fetch('/snare.mp3');
        const arrayBuffer = await response.arrayBuffer();
        snareBuffer.value = await audioContext.value.decodeAudioData(arrayBuffer);
      } catch (error) {
        console.error('Failed to load snare sample:', error);
      }
    }
  };

  // Play the snare sample with pitch and volume variations
  const playDrumHit = (isRightHand: boolean, when: number, isGrace: boolean = false) => {
    if (!audioContext.value || !snareBuffer.value) return;

    const ctx = audioContext.value;

    // Create buffer source
    const source = ctx.createBufferSource();
    source.buffer = snareBuffer.value;

    // Gain control
    const gainNode = ctx.createGain();
    const volume = isGrace ? 0.4 : 1.0;
    gainNode.gain.setValueAtTime(volume, when);

    // Pitch shift using playback rate
    // Right hand slightly higher pitch, left slightly lower
    source.playbackRate.value = isRightHand ? 1.05 : 0.95;

    // For grace notes, slightly faster playback for shorter duration
    if (isGrace) {
      source.playbackRate.value *= 1.3;
    }

    // Connect and play
    source.connect(gainNode);
    gainNode.connect(ctx.destination);
    source.start(when);
  };

  // Play a metronome click (immediate playback version for count-in)
  const playMetronomeClick = (when: number = 0, isDownbeat: boolean = false) => {
    if (!audioContext.value) return;

    const ctx = audioContext.value;
    const playTime = when === 0 ? ctx.currentTime : when;

    const oscillator = ctx.createOscillator();
    const gainNode = ctx.createGain();

    oscillator.frequency.setValueAtTime(isDownbeat ? 1200 : 800, playTime);
    gainNode.gain.setValueAtTime(0.3, playTime);
    gainNode.gain.exponentialRampToValueAtTime(0.01, playTime + 0.05);

    oscillator.connect(gainNode);
    gainNode.connect(ctx.destination);

    oscillator.start(playTime);
    oscillator.stop(playTime + 0.05);
  };

  // Parse the sticking pattern into strokes with timing
  const parsePattern = (sticking: string, subdivision: string): Stroke[] => {
    const strokes: Stroke[] = [];
    const chars = sticking.split(' ').join('').split('');

    // Determine note duration based on subdivision
    let noteDuration = 1; // Relative to quarter note
    if (subdivision === '8th') {
      noteDuration = 0.5;
    } else if (subdivision === '16th') {
      noteDuration = 0.25;
    } else if (subdivision === 'triplet') {
      noteDuration = 1 / 3;
    }

    for (const char of chars) {
      const isLowerCase = char === char.toLowerCase();
      const hand = char.toUpperCase() as 'R' | 'L';

      if (hand === 'R' || hand === 'L') {
        strokes.push({
          hand,
          isGrace: isLowerCase,
          duration: noteDuration
        });
      }
    }

    return strokes;
  };

  // Schedule notes ahead of time for tighter timing
  const scheduleNote = (beatNumber: number, time: number) => {
    if (currentStrokeIndex >= pattern.length) {
      currentStrokeIndex = 0;
    }

    const stroke = pattern[currentStrokeIndex];
    if (!stroke) return;

    // Grace notes play slightly before the beat, but never before the current audio time
    const graceOffset = stroke.isGrace ? 0.03 : 0;
    const playTime = Math.max(audioContext.value?.currentTime || 0, time - graceOffset);

    playDrumHit(stroke.hand === 'R', playTime, stroke.isGrace);

    // Update the highlight index for UI synchronization
    const highlightIndex = currentStrokeIndex;
    const highlightDelay = (playTime - (audioContext.value?.currentTime || 0)) * 1000;

    setTimeout(() => {
      currentHighlightIndex.value = highlightIndex % pattern.length;
    }, Math.max(0, highlightDelay));

    // Metronome click on downbeats (every 4th beat)
    if (!stroke.isGrace && beatNumber % 4 === 0) {
      playMetronomeClick(time, true);
    }

    currentStrokeIndex++;
  };

  // Scheduler to maintain tight timing
  const scheduler = () => {
    if (!audioContext.value) return;

    const scheduleAheadTime = 0.1; // Schedule 100ms ahead

    while (nextNoteTime < audioContext.value.currentTime + scheduleAheadTime) {
      const currentStroke = pattern[currentStrokeIndex % pattern.length];
      if (!currentStroke) break;

      const strokeDuration = beatDuration * currentStroke.duration;

      scheduleNote(currentStrokeIndex, nextNoteTime);
      nextNoteTime += strokeDuration;
    }
  };

  // Start playing the pattern at the specified BPM
  const startPattern = async (sticking: string, bpm: number, subdivision: string = '16th') => {
    await initAudio();

    pattern = parsePattern(sticking, subdivision);

    if (pattern.length === 0) return;

    // Quarter note duration in seconds
    beatDuration = 60.0 / bpm;

    currentStrokeIndex = 0;
    nextNoteTime = audioContext.value!.currentTime;
    isPlaying.value = true;

    // Schedule notes every 25ms
    schedulerInterval = window.setInterval(() => scheduler(), 25);
  };

  // Stop playing the pattern
  const stopPattern = () => {
    if (schedulerInterval) {
      clearInterval(schedulerInterval);
      schedulerInterval = null;
    }
    isPlaying.value = false;
    currentStrokeIndex = 0;
    currentHighlightIndex.value = -1; // Reset highlight
  };

  // Pause playing the pattern (keeps position)
  const pausePattern = () => {
    if (schedulerInterval) {
      clearInterval(schedulerInterval);
      schedulerInterval = null;
    }
    isPlaying.value = false;
    // Don't reset highlight - keep showing where we paused
  };

  // Resume playing from current position
  const resumePattern = () => {
    if (!audioContext.value || pattern.length === 0) return;

    isPlaying.value = true;
    nextNoteTime = audioContext.value.currentTime;

    // Schedule notes every 25ms
    schedulerInterval = window.setInterval(() => scheduler(), 25);
  };

  // Cleanup on unmount
  onUnmounted(() => {
    stopPattern();
    if (audioContext.value) {
      audioContext.value.close();
    }
  });

  return {
    isPlaying,
    currentHighlightIndex,
    playDrumHit,
    playMetronomeClick,
    startPattern,
    stopPattern,
    pausePattern,
    resumePattern,
    initAudio,
  };
}
