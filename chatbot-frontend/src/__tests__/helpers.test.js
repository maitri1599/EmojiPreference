import { EMOJI_LEVELS, MAX_LENGTH, formatTime, getUserId } from '../helpers';

describe('helpers', () => {
  it('EMOJI_LEVELS has 5 items', () => {
    expect(EMOJI_LEVELS).toHaveLength(5);
  });

  it('MAX_LENGTH is 500', () => {
    expect(MAX_LENGTH).toBe(500);
  });

  it('formatTime returns a string', () => {
    const result = formatTime(new Date().toISOString());
    expect(typeof result).toBe('string');
  });

  it('getUserId returns the same id on repeated calls', () => {
    localStorage.clear();
    const id1 = getUserId();
    const id2 = getUserId();
    expect(id1).toBe(id2);
  });
});
