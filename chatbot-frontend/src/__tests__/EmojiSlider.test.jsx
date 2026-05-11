import { render, screen, fireEvent } from '@testing-library/react';
import EmojiSlider from '../components/EmojiSlider';

describe('EmojiSlider', () => {
  it('renders the dropdown', () => {
    render(<EmojiSlider value={0} onChange={() => {}} />);
    expect(screen.getByRole('combobox')).toBeInTheDocument();
  });

  it('calls onChange when selection changes', () => {
    const onChange = jest.fn();
    render(<EmojiSlider value={0} onChange={onChange} />);
    fireEvent.change(screen.getByRole('combobox'), { target: { value: '3' } });
    expect(onChange).toHaveBeenCalledWith(3);
  });
});
