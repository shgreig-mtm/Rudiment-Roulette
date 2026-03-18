<template>
  <div class="flex flex-col items-center justify-center min-h-screen bg-drum-dark text-white p-8">
    <!-- Header -->
    <div class="text-center mb-12">
      <h1 class="text-6xl font-bold text-drum-orange mb-4">🥁 Rudiment Roulette</h1>
      <p class="text-xl text-drum-gray">Spin the wheel of pain... I mean practice!</p>
    </div>

    <!-- Main Content -->
    <div v-if="!selectedRudiment" class="flex flex-col items-center">
      <!-- Skill Level Selector -->
      <div class="mb-8 w-80">
        <label class="block text-drum-gray text-lg mb-2 text-center">
          Select Your Skill Level
        </label>
        <select
          v-model.number="selectedSkillLevel"
          class="select-control"
        >
          <option :value="1">Beginner</option>
          <option :value="2">Intermediate</option>
          <option :value="3">Advanced</option>
        </select>
      </div>

      <!-- Direct Rudiment Selector -->
      <div class="mb-8 w-80">
        <label class="block text-drum-gray text-lg mb-2 text-center">
          Or Choose a Specific Rudiment
        </label>
        <select
          v-model.number="selectedRudimentId"
          class="select-control mb-3"
        >
          <option :value="null">-- Select a Rudiment --</option>
          <option 
            v-for="rudiment in availableRudiments" 
            :key="rudiment.id" 
            :value="rudiment.id"
          >
            {{ rudiment.name }}
          </option>
        </select>
        <button
          @click="selectRudiment"
          :disabled="!selectedRudimentId"
          class="btn-select"
        >
          Select This Rudiment
        </button>
      </div>

      <!-- Divider -->
      <div class="divider mb-8">
        <div class="divider-line"></div>
        <span class="divider-text">or</span>
        <div class="divider-line"></div>
      </div>

      <!-- Wheel Container -->
      <div class="relative mb-12">
        <!-- Pointer -->
        <div class="absolute -top-8 left-1/2 transform -translate-x-1/2 z-10">
          <div class="wheel-pointer"></div>
        </div>

        <!-- Spinning Wheel -->
        <div 
          @click="spinWheel"
          class="wheel wheel-container"
          :class="{ 
            'animate-spin-slow': isSpinning,
            'spinning': isSpinning
          }"
          :style="{ transform: `rotate(${rotation}deg)` }"
        >
          <div class="text-center pointer-events-none">
            <p class="text-4xl mb-2">🥁</p>
            <p class="text-2xl font-bold text-drum-orange-light">SPIN ME!</p>
          </div>
        </div>
      </div>

      <!-- Spin Button -->
      <button
        @click="spinWheel"
        :disabled="isSpinning"
        class="btn-primary"
      >
        {{ isSpinning ? 'SPINNING...' : 'SPIN THE WHEEL' }}
      </button>

      <!-- Loading State -->
      <div v-if="isSpinning" class="mt-8 text-drum-orange-light animate-pulse">
        <p class="text-xl">Consulting the drum gods...</p>
      </div>
    </div>

    <!-- Result Display -->
    <div v-else class="w-full max-w-4xl">
      <!-- Rudiment Card -->
      <div class="card-rudiment mb-8">
        <h2 class="text-4xl font-bold text-drum-orange mb-4">{{ selectedRudiment.name }}</h2>
        <div class="grid grid-cols-3 gap-4 mb-6 text-lg">
          <div>
            <span class="text-drum-gray">Category:</span>
            <span class="ml-2 font-semibold text-drum-orange-light">{{ selectedRudiment.category }}</span>
          </div>
          <div>
            <span class="text-drum-gray">Difficulty:</span>
            <span class="ml-2 font-semibold text-drum-orange-light">{{ difficultyLabel }}</span>
          </div>
          <div>
            <span class="text-drum-gray">Subdivision:</span>
            <span class="ml-2 font-semibold text-drum-orange-light">{{ selectedRudiment.subdivision || '16th' }} notes</span>
          </div>
        </div>

        <!-- Sheet Music Display -->
        <div v-if="selectedRudiment.sheetMusicUrl" class="card-sheet-music mb-6">
          <p class="text-drum-gray text-center text-sm mb-2 text-gray-700">Sheet Music:</p>
          <img 
            :src="selectedRudiment.sheetMusicUrl" 
            :alt="`${selectedRudiment.name} sheet music`"
            class="max-w-full h-auto mx-auto"
            style="max-height: 200px;"
          />
        </div>

        <div class="text-center">
          <p class="text-drum-gray mb-2">Sticking Pattern:</p>
          <p class="text-3xl font-mono tracking-widest text-white bg-drum-dark p-4 rounded">
            {{ selectedRudiment.sticking }}
          </p>
        </div>
      </div>

      <!-- Practice Controls -->
      <div v-if="!isDrilling" class="text-center">
        <!-- BPM Selector -->
        <div class="mb-6 max-w-md mx-auto">
          <label class="block text-drum-gray text-lg mb-2">
            Target BPM
          </label>
          <div class="flex items-center justify-center gap-4 mb-2">
            <button
              @click="decreaseBpm"
              :disabled="bpm <= 40"
              class="btn-bpm"
            >
              -
            </button>
            <p class="text-6xl font-bold text-drum-orange w-32 text-center">{{ bpm }}</p>
            <button
              @click="increaseBpm"
              :disabled="bpm >= 240"
              class="btn-bpm"
            >
              +
            </button>
          </div>
        </div>

        <!-- Duration Slider -->
        <div class="mb-6 max-w-md mx-auto">
          <label class="block text-drum-gray text-lg mb-2">
            Practice Duration: <span class="text-drum-orange font-bold">{{ formatDuration(drillDuration) }}</span>
          </label>
          <input
            type="range"
            v-model.number="drillDuration"
            min="10"
            max="600"
            step="10"
            class="w-full h-3 bg-drum-dark rounded-lg appearance-none cursor-pointer slider"
          />
          <div class="flex justify-between text-sm text-drum-gray mt-1">
            <span>10s</span>
            <span>10min</span>
          </div>
        </div>

        <button
          @click="startDrill"
          class="btn-primary mb-4"
        >
          🔥 START THE PAIN 🔥
        </button>
        <button
          @click="resetWheel"
          class="btn-secondary block mx-auto"
        >
          Spin Again
        </button>
      </div>

      <!-- Practice Session -->
      <div v-else class="card-drill">
        <!-- Count-In Overlay -->
        <div v-if="isCountingIn" class="count-in-overlay">
          <div class="text-center">
            <p class="text-drum-gray text-2xl mb-4">Get Ready!</p>
            <p class="count-in-number">{{ countInNumber }}</p>
          </div>
        </div>

        <!-- BPM Display with Controls -->
        <div class="text-center mb-8">
          <p class="text-drum-gray text-xl mb-2">Target BPM</p>
          <div class="flex items-center justify-center gap-4 mb-4">
            <button
              @click="decreaseBpm"
              :disabled="bpm <= 40"
              class="btn-bpm-large"
            >
              -
            </button>
            <p class="text-8xl font-bold text-drum-orange">{{ bpm }}</p>
            <button
              @click="increaseBpm"
              :disabled="bpm >= 240"
              class="btn-bpm-large"
            >
              +
            </button>
          </div>
          <!-- Audio Indicator - Always reserve space to prevent UI shift -->
          <div class="audio-indicator">
            <template v-if="isPlaying">
              <span class="audio-dot playing"></span>
              <span class="audio-text playing">Audio Playing</span>
            </template>
            <template v-else-if="isPaused">
              <span class="audio-dot paused"></span>
              <span class="audio-text paused">Audio Paused</span>
            </template>
            <template v-else>
              <span class="audio-text text-transparent">Placeholder</span>
            </template>
          </div>
        </div>

        <!-- Sticking Pattern -->
        <div class="mb-8 text-center">
          <p class="text-drum-gray text-xl mb-4">Follow the Pattern:</p>
          <div class="flex justify-center gap-4 flex-wrap">
            <span 
              v-for="(stroke, index) in stickingArray" 
              :key="index"
              class="stroke-indicator"
              :class="[
                currentHighlightIndex === index && isPlaying ? 'active' : '',
                currentHighlightIndex === index && isPaused ? 'paused' : '',
                currentHighlightIndex !== index ? 'inactive' : '',
                stroke === 'R' || stroke === 'r' ? 'right-hand' : 'left-hand'
              ]"
            >
              {{ stroke }}
            </span>
          </div>
        </div>

        <!-- Progress Bar -->
        <div class="mb-8">
          <div class="flex justify-between text-drum-gray mb-2">
            <span>Progress</span>
            <span>{{ timeRemaining }}s / {{ drillDuration }}s</span>
          </div>
          <div class="progress-container">
            <div 
              class="progress-bar"
              :style="{ width: `${progressPercent}%` }"
            ></div>
          </div>
        </div>

        <!-- Drill Complete -->
        <div v-if="drillComplete" class="text-center mb-4">
          <p class="text-3xl font-bold text-drum-orange animate-pulse">🎉 DRILL COMPLETE! 🎉</p>
          <p class="text-xl text-drum-gray mt-2">You survived the pain!</p>
        </div>

        <!-- Control Buttons -->
        <div class="text-center flex gap-4 justify-center">
          <!-- Pause/Resume Button -->
          <button
            v-if="!drillComplete"
            @click="isPaused ? resumeDrill() : pauseDrill()"
            :class="isPaused ? 'btn-resume' : 'btn-pause'"
          >
            {{ isPaused ? '▶️ Resume' : '⏸️ Pause' }}
          </button>

          <!-- Stop Button -->
          <button
            @click="stopDrill"
            class="btn-stop"
          >
            {{ drillComplete ? 'Done' : 'Stop Drill' }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted } from 'vue';
import { getRandomRudiment, getAllRudiments, type Rudiment, type DifficultyLevel, DifficultyLabels } from '../services/api';
import { useDrumAudio } from '../composables/useDrumAudio';

const isSpinning = ref(false);
const rotation = ref(0);
const selectedRudiment = ref<Rudiment | null>(null);
const selectedSkillLevel = ref<DifficultyLevel>(1); // Default to Beginner (1)
const selectedRudimentId = ref<number | null>(null); // For direct selection
const availableRudiments = ref<Rudiment[]>([]);
const isDrilling = ref(false);
const isPaused = ref(false);
const isCountingIn = ref(false);
const countInNumber = ref(0);
const bpm = ref(80);
const drillDuration = ref(60); // Default 60 seconds
const timeRemaining = ref(60);
const drillComplete = ref(false);
let drillInterval: number | null = null;
let countInInterval: number | null = null;

const { isPlaying, currentHighlightIndex, startPattern, stopPattern, pausePattern, resumePattern, playMetronomeClick, initAudio } = useDrumAudio();

// Fetch rudiments when skill level changes
watch(selectedSkillLevel, async () => {
  await loadRudiments();
});

// Load rudiments on mount
onMounted(async () => {
  await loadRudiments();
});

async function loadRudiments() {
  try {
    availableRudiments.value = await getAllRudiments(selectedSkillLevel.value);
    // Sort by name for easier selection
    availableRudiments.value.sort((a, b) => a.name.localeCompare(b.name));
  } catch (error) {
    console.error('Failed to load rudiments:', error);
  }
}

const progressPercent = computed(() => {
  return ((drillDuration.value - timeRemaining.value) / drillDuration.value) * 100;
});

const stickingArray = computed(() => {
  if (!selectedRudiment.value) return [];
  return selectedRudiment.value.sticking.split(' ').join('').split('');
});

const difficultyLabel = computed(() => {
  return selectedRudiment.value ? DifficultyLabels[selectedRudiment.value.difficulty as DifficultyLevel] : '';
});

async function spinWheel() {
  isSpinning.value = true;

  // Random rotation between 1080 and 1800 degrees (3-5 full spins)
  const spins = Math.floor(Math.random() * 3 + 3);
  rotation.value += spins * 360 + Math.random() * 360;

  try {
    // Fetch rudiment while wheel is spinning, filtered by skill level
    const rudiment = await getRandomRudiment(selectedSkillLevel.value);

    // Wait for the spin animation to complete
    setTimeout(() => {
      selectedRudiment.value = rudiment;
      isSpinning.value = false;
    }, 2000);
  } catch (error) {
    console.error('Failed to fetch rudiment:', error);
    isSpinning.value = false;
    alert('Failed to fetch rudiment. Make sure the API is running!');
  }
}

function selectRudiment() {
  if (selectedRudimentId.value) {
    const rudiment = availableRudiments.value.find(r => r.id === selectedRudimentId.value);
    if (rudiment) {
      selectedRudiment.value = rudiment;
    }
  }
}

async function startDrill() {
  isDrilling.value = true;
  isCountingIn.value = true;
  drillComplete.value = false;
  timeRemaining.value = drillDuration.value;

  // Initialize audio first
  await initAudio();

  // Calculate number of count-in bars (1 bar = 4 beats)
  // Minimum 2 bars, add more bars for faster tempos
  const minBars = bpm.value >= 120 ? 3 : 2;
  const totalBeats = minBars * 4;

  // Calculate beat duration in milliseconds
  const beatDurationMs = (60 / bpm.value) * 1000;

  let currentBeat = 1;
  countInNumber.value = 1;

  // Play first metronome click immediately
  playMetronomeClick(0, true);

  // Count-in with metronome clicks
  countInInterval = window.setInterval(() => {
    currentBeat++;
    const beatInBar = ((currentBeat - 1) % 4) + 1;
    countInNumber.value = beatInBar;

    // Play metronome click (downbeat on beat 1)
    playMetronomeClick(0, beatInBar === 1);

    if (currentBeat > totalBeats) {
      // Count-in complete, start the actual drill
      if (countInInterval) {
        clearInterval(countInInterval);
        countInInterval = null;
      }
      isCountingIn.value = false;
      startActualDrill();
    }
  }, beatDurationMs);
}

function startActualDrill() {
  // Start playing the drum pattern
  if (selectedRudiment.value) {
    startPattern(
      selectedRudiment.value.sticking, 
      bpm.value, 
      selectedRudiment.value.subdivision || '16th'
    );
  }

  drillInterval = window.setInterval(() => {
    timeRemaining.value--;

    if (timeRemaining.value <= 0) {
      drillComplete.value = true;
      stopPattern();
      if (drillInterval) {
        clearInterval(drillInterval);
      }
    }
  }, 1000);
}

function stopDrill() {
  if (drillInterval) {
    clearInterval(drillInterval);
  }
  if (countInInterval) {
    clearInterval(countInInterval);
  }
  stopPattern();
  isDrilling.value = false;
  isPaused.value = false;
  isCountingIn.value = false;
  drillComplete.value = false;
  timeRemaining.value = drillDuration.value;
}

function pauseDrill() {
  isPaused.value = true;

  // Pause the timer
  if (drillInterval) {
    clearInterval(drillInterval);
    drillInterval = null;
  }

  // Pause the audio
  pausePattern();
}

function resumeDrill() {
  isPaused.value = false;

  // Resume the timer
  drillInterval = window.setInterval(() => {
    timeRemaining.value--;

    if (timeRemaining.value <= 0) {
      drillComplete.value = true;
      stopPattern();
      if (drillInterval) {
        clearInterval(drillInterval);
      }
    }
  }, 1000);

  // Resume the audio
  resumePattern();
}

function resetWheel() {
  selectedRudiment.value = null;
  stopDrill();
}

function increaseBpm() {
  if (bpm.value < 240) {
    bpm.value += 5;
    if (isPlaying.value && selectedRudiment.value) {
      stopPattern();
      startPattern(
        selectedRudiment.value.sticking, 
        bpm.value, 
        selectedRudiment.value.subdivision || '16th'
      );
    }
  }
}

function decreaseBpm() {
  if (bpm.value > 40) {
    bpm.value -= 5;
    if (isPlaying.value && selectedRudiment.value) {
      stopPattern();
      startPattern(
        selectedRudiment.value.sticking, 
        bpm.value, 
        selectedRudiment.value.subdivision || '16th'
      );
    }
  }
}

function formatDuration(seconds: number): string {
  if (seconds < 60) {
    return `${seconds}s`;
  }
  const minutes = Math.floor(seconds / 60);
  const remainingSeconds = seconds % 60;
  if (remainingSeconds === 0) {
    return `${minutes}min`;
  }
  return `${minutes}min ${remainingSeconds}s`;
}
</script>

<style scoped src="./Wheel.css"></style>
