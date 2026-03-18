# 🥁 Rudiment Roulette

An interactive web application for drummers to practice the 40 Percussive Arts Society (PAS) rudiments with precision timing, visual feedback, and audio playback.

[![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Vue.js](https://img.shields.io/badge/Vue.js-3-4FC08D?logo=vue.js)](https://vuejs.org/)
[![TypeScript](https://img.shields.io/badge/TypeScript-5-3178C6?logo=typescript)](https://www.typescriptlang.org/)
[![Tailwind CSS](https://img.shields.io/badge/Tailwind-3-38B2AC?logo=tailwind-css)](https://tailwindcss.com/)

---

## 🎯 Features

### 🎲 Interactive Practice
- **Spin the Wheel** - Randomize your practice routine
- **Direct Selection** - Choose specific rudiments to work on
- **Skill Level Filtering** - Beginner, Intermediate, or Advanced

### 🎵 Audio Playback
- Real **TR-808 snare drum samples** for authentic sound
- **Pitch-shifted audio** to differentiate left/right hand strikes
- **Precision timing** with Web Audio API scheduling
- **Grace note support** - Properly timed flams, drags, and ruffs
- **Metronome count-in** - Get ready before each drill

### 📊 Visual Feedback
- **Real-time stroke highlighting** synchronized with audio
- **BPM controls** - Practice from 40 to 240 BPM
- **Adjustable duration** - 10 seconds to 10 minutes
- **Progress tracking** with visual progress bar
- **Pause/Resume** functionality

### 🎼 Comprehensive Rudiment Library
- All **40 PAS rudiments** included
- Support for multiple note subdivisions (8th, 16th, triplets)
- Accurate sticking patterns with proper accents
- Sheet music display (infrastructure ready)

---

## 🚀 Quick Start

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/) and npm

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/shgreig-mtm/Rudiment-Roulette.git
   cd Rudiment-Roulette
   ```

2. **Start the .NET API**
   ```bash
   cd RudimentRoulette.Web
   dotnet run
   ```
   The API will run at `https://localhost:7163`

3. **Start the Vue frontend** (in a new terminal)
   ```bash
   cd RudimentRoulette.Client
   npm install
   npm run dev
   ```
   The app will open at `http://localhost:5173`

---

## 🏗️ Architecture

### Backend (.NET 10)
- **ASP.NET Core Web API** with minimal API pattern
- **RESTful endpoints** for rudiment data
- **Enum-based difficulty filtering** (inclusive levels)
- **Static data store** for the 40 PAS rudiments

### Frontend (Vue 3 + TypeScript)
- **Vue 3 Composition API** with `<script setup>`
- **TypeScript** for type safety
- **Tailwind CSS** for styling
- **Vite** for fast development and optimized builds
- **Web Audio API** for precise audio scheduling
- **Custom composables** (useDrumAudio) for reusable logic

### Project Structure
```
Rudiment-Roulette/
├── RudimentRoulette.Web/          # .NET API
│   ├── Controllers/
│   │   └── RudimentsController.cs # API endpoints
│   ├── Models/
│   │   ├── Rudiment.cs            # Core model
│   │   └── DifficultyLevel.cs     # Enum (1=Beginner, 2=Intermediate, 3=Advanced)
│   ├── Data/
│   │   └── RudimentStore.cs       # 40 rudiment definitions
│   └── Program.cs                 # App configuration
│
└── RudimentRoulette.Client/       # Vue 3 SPA
    ├── src/
    │   ├── components/
    │   │   ├── Wheel.vue          # Main component
    │   │   └── Wheel.css          # Component styles
    │   ├── composables/
    │   │   └── useDrumAudio.ts    # Audio playback logic
    │   ├── services/
    │   │   └── api.ts             # API client
    │   └── assets/
    │       └── main.css           # Tailwind & theme
    └── public/
        └── snare.mp3              # TR-808 sample
```

---

## 🎮 How to Use

1. **Select Your Skill Level**
   - Choose Beginner, Intermediate, or Advanced
   - Higher levels include all lower-level rudiments

2. **Choose a Practice Mode**
   - **Spin the Wheel** for random selection
   - **Pick from Dropdown** for targeted practice

3. **Configure Your Drill**
   - Set your target BPM (40-240)
   - Choose practice duration (10s - 10min)

4. **Start Practicing**
   - Watch the metronome count-in
   - Follow the highlighted sticking pattern
   - Listen to the synchronized audio
   - Adjust BPM during drill if needed
   - Pause/resume as needed

---

## 🎼 Rudiment Categories

The app includes all 40 PAS rudiments across these categories:

- **Roll Rudiments** - Single Stroke Roll, Double Stroke Roll, etc.
- **Diddle Rudiments** - Paradiddle, Double Paradiddle, etc.
- **Flam Rudiments** - Flam, Flam Accent, Flamacue, etc.
- **Drag Rudiments** - Drag, Single Drag Tap, Lesson 25, etc.

Each rudiment includes:
- ✅ Name and category
- ✅ Difficulty level
- ✅ Sticking pattern
- ✅ Note subdivision
- 🔜 Sheet music (infrastructure ready)

---

## 🛠️ Development

### Available Scripts

**Backend (.NET)**
```bash
dotnet run              # Start development server
dotnet build            # Build the project
```

**Frontend (Vue)**
```bash
npm run dev             # Start dev server with HMR
npm run build           # Build for production
npm run type-check      # Run TypeScript type checking
npm run preview         # Preview production build
```

### Code Style

- **Vue Components**: CSS extracted to separate `.css` files
- **TypeScript**: Strict mode enabled
- **Tailwind**: Custom drum theme with semantic classes
- **API**: Enum-based difficulty system for type safety

---

## 🎨 Customization

### Adding Sheet Music Images

1. Place images in `RudimentRoulette.Client/public/images/rudiments/`
2. Update `SheetMusicUrl` in `RudimentStore.cs`
3. See [SHEET_MUSIC_GUIDE.md](./SHEET_MUSIC_GUIDE.md) for detailed instructions

### Audio Samples

Replace `public/snare.mp3` with your own snare drum sample (WAV or MP3 format recommended).

### Theming

Edit `RudimentRoulette.Client/src/assets/main.css` to modify the drum theme colors:
- `drum-orange` - Primary accent color
- `drum-dark` - Background color
- `drum-slate` - Card backgrounds
- etc.

---

## 📝 API Endpoints

### GET /api/rudiments
Get all rudiments, optionally filtered by difficulty

**Query Parameters:**
- `difficulty` (optional): 1 (Beginner), 2 (Intermediate), or 3 (Advanced)

**Response:**
```json
[
  {
    "id": 1,
    "name": "Single Stroke Roll",
    "category": "Roll",
    "sticking": "RLRLRLRL",
    "difficulty": 1,
    "subdivision": "16th",
    "sheetMusicUrl": null
  }
]
```

### GET /api/rudiments/random
Get a random rudiment, optionally filtered by difficulty

**Query Parameters:**
- `difficulty` (optional): 1, 2, or 3

---

## 🐛 Known Issues

- PostCSS warning about `@import` order in main.css (cosmetic only, doesn't affect functionality)

---

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

### Ideas for Contribution
- Add sheet music images for all 40 rudiments
- Implement tempo ramping (gradual BPM increase)
- Add more drum sounds (hi-hat, kick, rim click)
- Create practice statistics/tracking
- Add different metronome sounds
- Implement rudiment difficulty ratings/scoring

---

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

- **Percussive Arts Society (PAS)** for standardizing the 40 rudiments
- **TR-808** drum machine for the iconic snare sound
- **Vue.js Team** for the amazing framework
- **Tailwind CSS** for the utility-first CSS framework

---

## 📧 Contact

Stuart Greig - [@shgreig-mtm](https://github.com/shgreig-mtm)

Project Link: [https://github.com/shgreig-mtm/Rudiment-Roulette](https://github.com/shgreig-mtm/Rudiment-Roulette)

---

**Built with ❤️ for drummers by drummers** 🥁
