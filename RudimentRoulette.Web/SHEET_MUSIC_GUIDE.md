# Rudiment Sheet Music Image Guide

## ✅ Setup Complete!

The code is now ready to support sheet music images for each rudiment.

---

## 📁 Directory Structure

Images should be placed in:
```
RudimentRoulette.Client/public/images/rudiments/
```

Images will be accessible at:
```
/images/rudiments/your-image.png
```

---

## 🎼 Where to Find Rudiment Sheet Music Images

### Official Sources:

1. **Percussive Arts Society (PAS)**
   - Website: https://www.pas.org/resources/rudiments
   - The official 40 PAS rudiments with standard notation
   - May need to screenshot or recreate

2. **Vic Firth**
   - Website: https://vicfirth.zildjian.com/education/40-essential-rudiments.html
   - Has notation for all 40 rudiments
   - High-quality images available

3. **OnlineDrummer.com**
   - Website: http://www.onlinedrummer.com/rudiments/
   - Individual rudiment pages with sheet music

4. **DrummerWorld**
   - Website: http://www.drummerworld.com/drummers/rudiments.html
   - Comprehensive rudiment notation

### Creating Your Own:

If you want to create your own images:
- **MuseScore** (Free) - Music notation software
- **Finale** or **Sibelius** - Professional notation software
- Export as PNG with transparent background
- Recommended size: 800x200 pixels

---

## 📝 Naming Convention

Suggested file naming (lowercase, hyphenated):
```
single-stroke-roll.png
single-stroke-four.png
double-paradiddle.png
flam-accent.png
drag-paradiddle-1.png
```

---

## 🔧 Adding Images to the Data

Once you have the images, update `Data/RudimentStore.cs`:

```csharp
new Rudiment
{
    Id = 1,
    Name = "Single Stroke Roll",
    Category = "Roll",
    Sticking = "RLRLRLRL",
    Difficulty = DifficultyLevel.Beginner,
    Subdivision = "16th",
    SheetMusicUrl = "/images/rudiments/single-stroke-roll.png"  // ← ADD THIS
},
```

---

## 🎨 Image Requirements

**Recommended specifications:**
- Format: PNG (with transparency) or JPG
- Width: 600-1000px
- Height: 150-300px
- Background: White or transparent
- DPI: 72-150 (web standard)
- File size: < 100KB per image

---

## 🚀 Quick Start Steps

1. **Download/Create Images**
   - Visit one of the sources above
   - Download or screenshot the sheet music
   - Save as PNG files

2. **Place Images**
   ```
   Copy images to: RudimentRoulette.Client/public/images/rudiments/
   ```

3. **Update Data**
   - Open `Data/RudimentStore.cs`
   - Add `SheetMusicUrl` to each rudiment
   - Use format: `/images/rudiments/filename.png`

4. **Test**
   - Run the app
   - Spin the wheel
   - Sheet music should appear above the sticking pattern

---

## 📦 Bulk Image Resources

**Option 1: Create a Script to Download**
You could create a PowerShell script to download from a source (if copyright allows)

**Option 2: Use Placeholder**
Create a placeholder image for now:
- Simple white background with text "Sheet Music Coming Soon"
- Replace later with actual notation

**Option 3: Commission/Purchase**
- Some drumming websites sell complete rudiment image packs
- Ensure you have rights to use them in your app

---

## 🔍 Example Implementation

Here's an example for the first 3 rudiments:

```csharp
new Rudiment
{
    Id = 1,
    Name = "Single Stroke Roll",
    Category = "Roll",
    Sticking = "RLRLRLRL",
    Difficulty = DifficultyLevel.Beginner,
    Subdivision = "16th",
    SheetMusicUrl = "/images/rudiments/single-stroke-roll.png"
},
new Rudiment
{
    Id = 2,
    Name = "Single Stroke Four",
    Category = "Roll",
    Sticking = "RLRL LRLR",
    Difficulty = DifficultyLevel.Beginner,
    Subdivision = "16th",
    SheetMusicUrl = "/images/rudiments/single-stroke-four.png"
},
new Rudiment
{
    Id = 3,
    Name = "Single Stroke Seven",
    Category = "Roll",
    Sticking = "RLRLRLR LRLRLRL",
    Difficulty = DifficultyLevel.Intermediate,
    Subdivision = "16th",
    SheetMusicUrl = "/images/rudiments/single-stroke-seven.png"
},
```

---

## 💡 Tips

- **Start Small**: Add images for your most-used rudiments first
- **Consistency**: Keep image style consistent across all rudiments
- **Copyright**: Ensure you have rights to use any images
- **Fallback**: The UI will gracefully hide the image section if `SheetMusicUrl` is null/empty
- **Performance**: Optimize images before adding (use tools like TinyPNG)

---

## 🎯 Next Steps

1. Visit Vic Firth website (easiest source)
2. Download/screenshot the first few rudiment notations
3. Save to `public/images/rudiments/`
4. Update `RudimentStore.cs` with the URLs
5. Test and repeat for remaining rudiments

The UI is ready - just add the images! 🥁
