import { formatTime } from '../helpers';
import styles from './ChatMessage.module.css';

export default function ChatMessage({ message }) {
  const isUser = message.sender === 'user';

  return (
    <div className={`${styles.row} ${isUser ? styles.userRow : styles.botRow}`}>
      <span className={styles.label}>{isUser ? 'you:' : 'bot:'}</span>
      <span className={styles.text}>{message.text}</span>
      <span className={styles.time}>{formatTime(message.timestamp)}</span>
    </div>
  );
}
