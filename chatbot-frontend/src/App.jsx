import { useState, useCallback, useEffect } from 'react';
import { v4 as uuidv4 } from 'uuid';
import Header from './components/Header';
import ChatWindow from './components/ChatWindow';
import MessageInput from './components/MessageInput';
import EmojiSlider from './components/EmojiSlider';
import { sendMessage, savePreference, registerUser } from './api';
import { useLocalStorage, getUserId } from './helpers';
import './App.css';

const userId = getUserId();

export default function App() {
  const [messages, setMessages] = useLocalStorage('chatbot_messages', []);
  const [emojiLevel, setEmojiLevel] = useLocalStorage('chatbot_emoji_level', 2);
  const [userNum, setUserNum] = useLocalStorage('chatbot_user_number', null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    registerUser(userId)
      .then(num => setUserNum(num))
      .catch(() => {});
  }, []);

  const handleSend = useCallback(async (text) => {
    const userMsg = {
      id: uuidv4(),
      sender: 'user',
      text,
      timestamp: new Date().toISOString(),
    };
    setMessages(prev => [...prev, userMsg]);
    setLoading(true);
    setError('');

    try {
      const data = await sendMessage(text, emojiLevel, userId);
      const botMsg = {
        id: uuidv4(),
        sender: 'bot',
        text: data.reply,
        timestamp: data.timestamp || new Date().toISOString(),
      };
      setMessages(prev => [...prev, botMsg]);
    } catch (err) {
      setError(err.response?.data?.error || 'Could not reach the server.');
    } finally {
      setLoading(false);
    }
  }, [emojiLevel, setMessages]);

  const handleEmoji = useCallback(async (level) => {
    setEmojiLevel(level);
    try {
      await savePreference(userId, level);
    } catch {
    }
  }, [setEmojiLevel]);

  const clearChat = () => setMessages([]);

  return (
    <div className="app">
      <Header onClearChat={clearChat} userNum={userNum} />
      <ChatWindow messages={messages} loading={loading} />
      {error && <div className="apiError">{error}</div>}
      <EmojiSlider value={emojiLevel} onChange={handleEmoji} />
      <MessageInput onSend={handleSend} disabled={loading} />
    </div>
  );
}
