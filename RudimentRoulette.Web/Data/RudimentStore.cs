using RudimentRoulette.Web.Models;

namespace RudimentRoulette.Web.Data;

public static class RudimentStore
{
    public static List<Rudiment> Rudiments { get; } =
    [
        // ROLL RUDIMENTS (13)
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
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 3,
            Name = "Single Stroke Seven",
            Category = "Roll",
            Sticking = "RLRLRLR LRLRLRL",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 4,
            Name = "Multiple Bounce Roll",
            Category = "Roll",
            Sticking = "RR LL RR LL",
            Difficulty = DifficultyLevel.Beginner,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 5,
            Name = "Triple Stroke Roll",
            Category = "Roll",
            Sticking = "RRRLLLRRRLLL",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "triplet"
        },
        new Rudiment
        {
            Id = 6,
            Name = "Double Stroke Open Roll",
            Category = "Roll",
            Sticking = "RRLLRRLL",
            Difficulty = DifficultyLevel.Beginner,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 7,
            Name = "Five Stroke Roll",
            Category = "Roll",
            Sticking = "RRLL R LLRR L",
            Difficulty = DifficultyLevel.Beginner,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 8,
            Name = "Six Stroke Roll",
            Category = "Roll",
            Sticking = "RLRLRR LRLRLL",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 9,
            Name = "Seven Stroke Roll",
            Category = "Roll",
            Sticking = "RRLLRRL LLRRLLR",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 10,
            Name = "Nine Stroke Roll",
            Category = "Roll",
            Sticking = "RRLLRRLLR",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 11,
            Name = "Ten Stroke Roll",
            Category = "Roll",
            Sticking = "RRLLRRLLRL",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 12,
            Name = "Eleven Stroke Roll",
            Category = "Roll",
            Sticking = "RRLLRRLLRRL",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 13,
            Name = "Thirteen Stroke Roll",
            Category = "Roll",
            Sticking = "RRLLRRLLRRLLR",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 14,
            Name = "Fifteen Stroke Roll",
            Category = "Roll",
            Sticking = "RRLLRRLLRRLLRRL",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 15,
            Name = "Seventeen Stroke Roll",
            Category = "Roll",
            Sticking = "RRLLRRLLRRLLRRLLR",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "16th"
        },

        // DIDDLE RUDIMENTS (7)
        new Rudiment
        {
            Id = 16,
            Name = "Single Paradiddle",
            Category = "Diddle",
            Sticking = "RLRR LRLL",
            Difficulty = DifficultyLevel.Beginner,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 17,
            Name = "Double Paradiddle",
            Category = "Diddle",
            Sticking = "RLRLRR LRLRLL",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 18,
            Name = "Triple Paradiddle",
            Category = "Diddle",
            Sticking = "RLRLRLRR LRLRLRLL",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 19,
            Name = "Paradiddle-Diddle",
            Category = "Diddle",
            Sticking = "RLRRLL LRLLRR",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },

        // FLAM RUDIMENTS (8)
        new Rudiment
        {
            Id = 20,
            Name = "Flam",
            Category = "Flam",
            Sticking = "lR rL",
            Difficulty = DifficultyLevel.Beginner,
            Subdivision = "8th"
        },
        new Rudiment
        {
            Id = 21,
            Name = "Flam Accent",
            Category = "Flam",
            Sticking = "lR L rL R",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "triplet"
        },
        new Rudiment
        {
            Id = 22,
            Name = "Flam Tap",
            Category = "Flam",
            Sticking = "lR R rL L",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "triplet"
        },
        new Rudiment
        {
            Id = 23,
            Name = "Flamacue",
            Category = "Flam",
            Sticking = "lR L RL R",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 24,
            Name = "Flam Paradiddle",
            Category = "Flam",
            Sticking = "lR L R R rL R L L",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 25,
            Name = "Single Flammed Mill",
            Category = "Flam",
            Sticking = "lR L rL R R lR L rL R L",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 26,
            Name = "Flam Paradiddle-Diddle",
            Category = "Flam",
            Sticking = "lR L R R L L rL R L L R R",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 27,
            Name = "Pataflafla",
            Category = "Flam",
            Sticking = "lR L rL R L R rL R lR L R L",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 28,
            Name = "Swiss Army Triplet",
            Category = "Flam",
            Sticking = "lR R rL L R L R L",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "triplet"
        },
        new Rudiment
        {
            Id = 29,
            Name = "Inverted Flam Tap",
            Category = "Flam",
            Sticking = "lR lR R rL rL L",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "triplet"
        },
        new Rudiment
        {
            Id = 30,
            Name = "Flam Drag",
            Category = "Flam",
            Sticking = "lR ll R rL rr L",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "16th"
        },

        // DRAG RUDIMENTS (12)
        new Rudiment
        {
            Id = 31,
            Name = "Drag",
            Category = "Drag",
            Sticking = "llR rrL",
            Difficulty = DifficultyLevel.Beginner,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 32,
            Name = "Single Drag Tap",
            Category = "Drag",
            Sticking = "llR L rrL R",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 33,
            Name = "Double Drag Tap",
            Category = "Drag",
            Sticking = "llR L llR L rrL R rrL R",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 34,
            Name = "Lesson 25",
            Category = "Drag",
            Sticking = "llR llR L rrL rrL R",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 35,
            Name = "Single Dragadiddle",
            Category = "Drag",
            Sticking = "RR llR L llR L LL rrL R rrL R",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 36,
            Name = "Drag Paradiddle #1",
            Category = "Drag",
            Sticking = "RllR L R R LrrL R L L",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 37,
            Name = "Drag Paradiddle #2",
            Category = "Drag",
            Sticking = "RllR L RllR L R R LrrL R LrrL R L L",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "16th"
        },
        new Rudiment
        {
            Id = 38,
            Name = "Single Ratamacue",
            Category = "Drag",
            Sticking = "llR L R L R",
            Difficulty = DifficultyLevel.Intermediate,
            Subdivision = "triplet"
        },
        new Rudiment
        {
            Id = 39,
            Name = "Double Ratamacue",
            Category = "Drag",
            Sticking = "llR L llR L R L R",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "triplet"
        },
        new Rudiment
        {
            Id = 40,
            Name = "Triple Ratamacue",
            Category = "Drag",
            Sticking = "llR L llR L llR L R L R",
            Difficulty = DifficultyLevel.Advanced,
            Subdivision = "triplet"
        }
    ];
}
