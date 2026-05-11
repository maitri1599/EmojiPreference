import { render, screen } from '@testing-library/react';
import ChatMessage from '../components/ChatMessage';

describe('ChatMessage', () => {
  it('shows user message', () => {
    const msg = { id: '1', sender: 'user', text: 'hello', timestamp: new Date().toISOString() };
    render(<ChatMessage message={msg} />);
    expect(screen.getByText('hello')).toBeInTheDocument();
    expect(screen.getByText('you:')).toBeInTheDocument();
  });

  it('shows bot message', () => {
    const msg = { id: '2', sender: 'bot', text: 'hi back', timestamp: new Date().toISOString() };
    render(<ChatMessage message={msg} />);
    expect(screen.getByText('hi back')).toBeInTheDocument();
    expect(screen.getByText('bot:')).toBeInTheDocument();
  });
});
