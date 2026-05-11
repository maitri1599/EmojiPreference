import styles from './Loader.module.css';

export default function Loader() {
  return (
    <div className={styles.row}>
      <span className={styles.label}>bot:</span>
      <span className={styles.text}>thinking...</span>
    </div>
  );
}
