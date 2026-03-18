// API configuration
const API_BASE_URL = 'https://localhost:44354/api';

export interface Rudiment {
  id: number;
  name: string;
  category: string;
  sticking: string;
  difficulty: number; // 1 = Beginner, 2 = Intermediate, 3 = Advanced
  subdivision: string;
  sheetMusicUrl?: string; // Optional URL to sheet music image
}

export type DifficultyLevel = 1 | 2 | 3;

export const DifficultyLabels = {
  1: 'Beginner',
  2: 'Intermediate',
  3: 'Advanced'
} as const;

// Fetch a random rudiment from the backend
export async function getRandomRudiment(difficulty: DifficultyLevel): Promise<Rudiment> {
  try {
    const params = new URLSearchParams();
    params.append('difficulty', difficulty.toString());

    const url = `${API_BASE_URL}/rudiments/random?${params.toString()}`;

    const response = await fetch(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error('Error fetching random rudiment:', error);
    throw error;
  }
}

// Fetch all rudiments (optional, for future use)
export async function getAllRudiments(difficulty?: DifficultyLevel): Promise<Rudiment[]> {
  try {
    const params = new URLSearchParams();
    if (difficulty) {
      params.append('difficulty', difficulty.toString());
    }

    const url = `${API_BASE_URL}/rudiments${params.toString() ? `?${params.toString()}` : ''}`;

    const response = await fetch(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error('Error fetching rudiments:', error);
    throw error;
  }
}
