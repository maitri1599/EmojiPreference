import { useEffect, useRef } from 'react';
import ChatMessage from './ChatMessage';
import Loader from './Loader';
import styles from './ChatWindow.module.css';

export default function ChatWindow({ messages, loading }) {
  const bottomRef = useRef(null);

  useEffect(() => {
    bottomRef.current?.scrollIntoView({ behavior: 'smooth' });
  }, [messages, loading]);

  return (
    <div className={styles.window}>
      {messages.length === 0 && (
        <p className={styles.empty}>No messages yet. Say something below.</p>
      )}
      {messages.map((msg) => (
        <ChatMessage key={msg.id} message={msg} />
      ))}
      {loading && <Loader />}
      <div ref={bottomRef} />
    </div>
  );
}
