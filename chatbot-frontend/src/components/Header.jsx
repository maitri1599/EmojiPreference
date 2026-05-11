import styles from './Header.module.css';

export default function Header({ onClearChat, userNum }) {
  return (
    <header className={styles.header}>
      <div>
        <h2 className={styles.title}>Your Assistant</h2>
        <p className={styles.welcome}>
          Welcome, {userNum !== null ? `User ${userNum}` : '...'}
        </p>
      </div>
      <button className={styles.clearBtn} onClick={onClearChat}>
        Clear
      </button>
    </header>
  );
}
