import { render, screen, fireEvent } from '@testing-library/react';
import Header from '../components/Header';

describe('Header', () => {
  it('shows the app title', () => {
    render(<Header onClearChat={() => {}} userNum={1} />);
    expect(screen.getByText('Your Assistant')).toBeInTheDocument();
  });

  it('shows the user number', () => {
    render(<Header onClearChat={() => {}} userNum={2} />);
    expect(screen.getByText('Welcome, User 2')).toBeInTheDocument();
  });

  it('clear button works', () => {
    const mockClear = jest.fn();
    render(<Header onClearChat={mockClear} userNum={1} />);
    fireEvent.click(screen.getByText('Clear'));
    expect(mockClear).toHaveBeenCalled();
  });
});
