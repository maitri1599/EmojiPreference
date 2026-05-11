import { useState } from 'react';
import { v4 as uuidv4 } from 'uuid';

export const EMOJI_LEVELS = [
  { value: 0, label: 'None', indicator: '😐' },
  { value: 1, label: 'Low', indicator: '🙂' },
  { value: 2, label: 'Moderate', indicator: '😊' },
  { value: 3, label: 'High', indicator: '😄' },
  { value: 4, label: 'Extreme', indicator: '🤩' },
];

export const MAX_LENGTH = 500;

export function getUserId() {
  const stored = localStorage.getItem('chatbot_user_id');
  if (stored) return stored;
  const id = uuidv4();
  localStorage.setItem('chatbot_user_id', id);
  return id;
}

export function formatTime(date) {
  return new Date(date).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
}

export function useLocalStorage(key, initial) {
  const [val, setVal] = useState(() => {
    const item = localStorage.getItem(key);
    return item ? JSON.parse(item) : initial;
  });

  const update = (value) => {
    setVal(prev => {
      const next = value instanceof Function ? value(prev) : value;
      localStorage.setItem(key, JSON.stringify(next));
      return next;
    });
  };

  return [val, update];
}
