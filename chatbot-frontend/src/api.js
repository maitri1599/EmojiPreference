import axios from 'axios';

const BASE_URL = process.env.REACT_APP_API_URL || 'http://localhost:5000';

const api = axios.create({
  baseURL: BASE_URL,
  headers: { 'Content-Type': 'application/json' },
});

export async function sendMessage(message, emojiLevel, userId) {
  const res = await api.post('/api/chat/send', { message, emojiLevel, userId });
  return res.data;
}

export async function savePreference(userId, emojiLevel) {
  await api.post('/api/chat/preference', { userId, emojiLevel });
}

export async function registerUser(userId) {
  const res = await api.post('/api/chat/register', { userId });
  return res.data.userNumber;
}
