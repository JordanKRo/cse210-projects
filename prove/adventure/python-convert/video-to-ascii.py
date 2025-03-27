import cv2
import numpy as np
import json
import argparse
from datetime import datetime

# ASCII character set from dark to light (more varied)
ASCII_CHARS = ['@', '#', 'B', '8', '&', 'W', 'M', 'o', 'a', 'h', 'k', 'b', 'd', 'p', 'q', 'w', 'm', 'Z', 'O', '0', 'Q', 'L', 'C', 'J', 'U', 'Y', 'X', 'z', 'c', 'v', 'u', 'n', 'x', 'r', 'j', 'f', 't', '/', '\\', '|', '(', ')', '1', '{', '}', '[', ']', '?', '-', '_', '+', '~', '<', '>', 'i', '!', 'l', 'I', ';', ':', ',', '"', '^', '`', "'", '.', ' ']

def resize_image(image, new_width=100):
    """Resize image to a specific width while maintaining aspect ratio."""
    height, width = image.shape[:2]
    ratio = height / width
    new_height = int(new_width * ratio * 0.5)  # Multiply by 0.5 to account for terminal aspect ratio
    return cv2.resize(image, (new_width, new_height))

def to_grayscale(image):
    """Convert image to grayscale."""
    return cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

def pixel_to_ascii(pixel):
    """Convert pixel value to ASCII character."""
    # Normalize pixel value to ASCII_CHARS length
    index = int(pixel * (len(ASCII_CHARS) - 1) / 255)
    return ASCII_CHARS[index]

def convert_frame_to_ascii(frame, width=100):
    """Convert a frame to ASCII art."""
    # Process the frame: resize and convert to grayscale
    resized_frame = resize_image(frame, width)
    grayscale_frame = to_grayscale(resized_frame)
    
    # Ensure proper contrast by normalizing the grayscale image
    min_pixel = np.min(grayscale_frame)
    max_pixel = np.max(grayscale_frame)
    
    # Skip normalization if the image has good contrast already
    if max_pixel > min_pixel + 30:
        normalized_frame = ((grayscale_frame - min_pixel) * 255 / (max_pixel - min_pixel)).astype(np.uint8)
    else:
        # Apply histogram equalization for low contrast frames
        normalized_frame = cv2.equalizeHist(grayscale_frame)
    
    # Convert each pixel to ASCII
    ascii_frame = []
    for row in normalized_frame:
        ascii_row = ''.join(pixel_to_ascii(pixel) for pixel in row)
        ascii_frame.append(ascii_row)
    
    # Debug info - print frame dimensions and unique character count
    if len(ascii_frame) > 0:
        unique_chars = set(''.join(ascii_frame))
        print(f"\rFrame dimensions: {len(ascii_frame)}x{len(ascii_frame[0])} with {len(unique_chars)} unique characters", end="")
    
    return ascii_frame

def convert_video_to_ascii(video_path, width=100, fps_target=None):
    """Convert video to ASCII frames and save to JSON."""
    # Open the video
    cap = cv2.VideoCapture(video_path)
    if not cap.isOpened():
        print(f"Error: Could not open video {video_path}")
        return None
    
    # Get video properties
    original_fps = cap.get(cv2.CAP_PROP_FPS)
    frame_count = int(cap.get(cv2.CAP_PROP_FRAME_COUNT))
    
    # Determine frame sampling rate
    fps = fps_target if fps_target else original_fps
    sampling_rate = max(1, int(original_fps / fps))
    
    # Prepare JSON data
    ascii_video = {
        "metadata": {
            "original_file": video_path,
            "width": width,
            "fps": fps,
            "original_fps": original_fps,
            "frame_count": frame_count,
            "converted_on": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        },
        "frames": []
    }
    
    # Process each frame
    frame_idx = 0
    while True:
        ret, frame = cap.read()
        if not ret:
            break
        
        # Only process every Nth frame based on sampling rate
        if frame_idx % sampling_rate == 0:
            ascii_frame = convert_frame_to_ascii(frame, width)
            ascii_video["frames"].append(ascii_frame)
            
            # Progress indicator
            completion = (frame_idx + 1) / frame_count * 100
            print(f"\rConverting: {completion:.1f}% complete", end="")
        
        frame_idx += 1
    
    # Clean up
    cap.release()
    print("\nConversion complete!")
    
    return ascii_video

def save_to_json(ascii_video, output_path):
    """Save ASCII video data to JSON file."""
    with open(output_path, 'w') as f:
        json.dump(ascii_video, f)
    print(f"ASCII video saved to {output_path}")

def main():
    parser = argparse.ArgumentParser(description='Convert video to ASCII art and save as JSON.')
    parser.add_argument('input', help='Input video file path')
    parser.add_argument('output', help='Output JSON file path')
    parser.add_argument('--width', type=int, default=100, help='Width of ASCII art (default: 100 characters)')
    parser.add_argument('--fps', type=float, default=None, help='Target FPS for the ASCII video (default: same as original)')
    
    args = parser.parse_args()
    
    # Convert video to ASCII
    ascii_video = convert_video_to_ascii(args.input, args.width, args.fps)
    
    if ascii_video:
        # Save to JSON
        save_to_json(ascii_video, args.output)

if __name__ == "__main__":
    main()