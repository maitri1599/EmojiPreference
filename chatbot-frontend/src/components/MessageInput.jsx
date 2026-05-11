import { useState } from 'react';
import { MAX_LENGTH } from '../helpers';
import styles from './MessageInput.module.css';

export default function MessageInput({ onSend, disabled }) {
  const [text, setText] = useState('');
  const [error, setError] = useState('');

  const handleSend = () => {
    const trimmed = text.trim();
    if (!trimmed) {
      setError('Message cannot be empty.');
      return;
    }
    if (trimmed.length > MAX_LENGTH) {
      setError(`Max ${MAX_LENGTH} characters allowed.`);
      return;
    }
    setError('');
    onSend(trimmed);
    setText('');
  };

  const handleKey = (e) => {
    if (e.key === 'Enter' && !e.shiftKey) {
      e.preventDefault();
      handleSend();
    }
  };

  return (
    <div className={styles.container}>
      {error && <p className={styles.error}>{error}</p>}
      <div className={styles.inputRow}>
        <textarea
          className={styles.textarea}
          value={text}
          onChange={(e) => { setText(e.target.value); setError(''); }}
          onKeyDown={handleKey}
          placeholder="Type a message... (Enter to send)"
          rows={1}
          maxLength={MAX_LENGTH + 1}
          disabled={disabled}
        />
        <button
          className={styles.sendBtn}
          onClick={handleSend}
          disabled={disabled || !text.trim()}
        >
          Send
        </button>
      </div>
      <div className={styles.charCount}>
        {text.length}/{MAX_LENGTH}
      </div>
    </div>
  );
}
