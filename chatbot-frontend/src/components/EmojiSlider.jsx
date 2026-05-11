import { EMOJI_LEVELS } from '../helpers';
import styles from './EmojiSlider.module.css';

export default function EmojiSlider({ value, onChange }) {
  return (
    <div className={styles.container}>
      <label htmlFor="emoji-level" className={styles.label}>
        Emoji level:
      </label>
      <select
        id="emoji-level"
        className={styles.select}
        value={value}
        onChange={(e) => onChange(Number(e.target.value))}
      >
        {EMOJI_LEVELS.map((level) => (
          <option key={level.value} value={level.value}>
            {level.indicator} {level.label}
          </option>
        ))}
      </select>
    </div>
  );
}
