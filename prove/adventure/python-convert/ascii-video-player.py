import json
import time
import os
import argparse
import sys
from datetime import datetime

def clear_screen():
    """Clear the terminal screen based on OS."""
    os.system('cls' if os.name == 'nt' else 'clear')

def display_frame(frame):
    """Display a single ASCII frame in the terminal."""
    frame_text = '\n'.join(frame)
    print(frame_text)

def load_ascii_video(json_path):
    """Load ASCII video data from a JSON file."""
    try:
        with open(json_path, 'r') as f:
            data = json.load(f)
        return data
    except (json.JSONDecodeError, FileNotFoundError) as e:
        print(f"Error loading JSON file: {e}")
        return None

def play_ascii_video(ascii_video, loop=False, speed_factor=1.0):
    """Play the ASCII video in the terminal."""
    frames = ascii_video["frames"]
    metadata = ascii_video["metadata"]
    
    # Calculate frame delay based on FPS
    fps = metadata.get("fps", 30)
    frame_delay = 1.0 / (fps * speed_factor)
    
    print(f"Playing ASCII video: {metadata.get('original_file', 'Unknown')}")
    print(f"FPS: {fps} | Frame Count: {len(frames)} | Speed: {speed_factor}x")
    print(f"Original creation: {metadata.get('converted_on', 'Unknown')}")
    print("Press Ctrl+C to exit")
    time.sleep(2)  # Give user time to read info
    
    try:
        while True:
            start_time = time.time()
            
            for i, frame in enumerate(frames):
                # Calculate the elapsed time since playback started
                elapsed = time.time() - start_time
                expected_frame_time = i * frame_delay
                
                # Sleep if we're ahead of schedule to maintain proper timing
                if elapsed < expected_frame_time:
                    time.sleep(expected_frame_time - elapsed)
                
                # Display the frame
                clear_screen()
                display_frame(frame)
                
                # Display playback info
                progress = (i + 1) / len(frames) * 100
                sys.stdout.write(f"\rPlayback: {progress:.1f}% ({i+1}/{len(frames)}) | Press Ctrl+C to exit")
                sys.stdout.flush()
            
            if not loop:
                break
                
            print("\nRestarting playback...")
            time.sleep(1)
            start_time = time.time()
            
    except KeyboardInterrupt:
        print("\nPlayback stopped by user")

def main():
    parser = argparse.ArgumentParser(description='Play ASCII art video from JSON file.')
    parser.add_argument('input', help='Input JSON file containing ASCII video')
    parser.add_argument('--loop', action='store_true', help='Loop the video playback')
    parser.add_argument('--speed', type=float, default=1.0, help='Playback speed factor (default: 1.0)')
    
    args = parser.parse_args()
    
    # Load the ASCII video
    ascii_video = load_ascii_video(args.input)
    
    if ascii_video:
        # Start playback
        play_ascii_video(ascii_video, args.loop, args.speed)

if __name__ == "__main__":
    main()
