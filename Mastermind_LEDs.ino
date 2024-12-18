/**
 * Travis Bode
 * 12/18/2024
 * 
 * This Arduino Sketch is designed to be used with my Mastermind C# app.
 * It controls yellow and green LEDs connected to the Arduino.
 * 
 * Here is the usage of the LEDs in relation to Mastermind:
 * 
 * Correct Mastermind color guess slots are indicated with a green LED corresponding to its slot in the guess row.
 * Misplaced Mastermind color guess slots are indicated with a yellow LED corresponding to its slot in the guess row.
 * Incorrect Mastermind color guess slots are indicated with the absence of a yellow or green LED in its guess slot.
 */

// this command turns off all the LEDs
const char RESET = 'R';

// this command performs a LED light show after the Mastermind game is won
const char WIN = 'W';

// this command turns on the red LED to indicate the Arduino is being used by the Mastermind app
const char ACTIVATED = 'A';

// this command turns off the red LED to indicate the Arduino is no longer being used by the Mastermind app
const char DEACTIVATED = 'D';

// this stores input received over the serial port from the computer
char input;

// these pins are the pins used by the LEDs on the Arduino
// the 4 green LEDs' pins
const int GREEN0_PIN = 13;
const int GREEN1_PIN = 11;
const int GREEN2_PIN = 9;
const int GREEN3_PIN = 6;

// the 4 yellow LEDs' pins
const int YELLOW0_PIN = 12;
const int YELLOW1_PIN = 10;
const int YELLOW2_PIN = 8;
const int YELLOW3_PIN = 7;

// the 1 red LED's pin
const int RED_PIN = 3;

// these codes are used to identify a pin (LED) by the Mastermind app.
// the 4 green LEDs' codes
const char GREEN0_CODE = '4';
const char GREEN1_CODE = '5';
const char GREEN2_CODE = '6';
const char GREEN3_CODE = '7';

// the 4 yellow LEDs' codes
const char YELLOW0_CODE = '0';
const char YELLOW1_CODE = '1';
const char YELLOW2_CODE = '2';
const char YELLOW3_CODE = '3';

// the 1 red LED's code
const char RED_CODE = '8';

// all the LEDs' pins (yellow and green; no red)
int allLEDs[] = {
  YELLOW0_PIN, YELLOW1_PIN, YELLOW2_PIN, YELLOW3_PIN,
  GREEN0_PIN, GREEN1_PIN, GREEN2_PIN, GREEN3_PIN
};

void setup() {
  // put your setup code here, to run once:
  
  // the green LEDs' pins
  pinMode(GREEN0_PIN, OUTPUT);
  pinMode(GREEN1_PIN, OUTPUT);
  pinMode(GREEN2_PIN, OUTPUT);
  pinMode(GREEN3_PIN, OUTPUT);


  // the yellow LEDs' pins
  pinMode(YELLOW0_PIN, OUTPUT);
  pinMode(YELLOW1_PIN, OUTPUT);
  pinMode(YELLOW2_PIN, OUTPUT);
  pinMode(YELLOW3_PIN, OUTPUT);

  // the red LED's pin
  pinMode(RED_PIN, OUTPUT);

  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  // read input from Mastermind
  input = Serial.read();

  // when the Arduino is being used by Mastermind, turn on the red LED to indicate that
  // since the LED is wired to a PWM pin, send less power through it to make the LED dimmer
  if (input == ACTIVATED) {
    analogWrite(RED_PIN, 50);
  }
  
  // check if the command was to turn off all the LEDs
  else if (input == RESET) {
    allLEDsOff();
  }

  // perform a LED light show when the user wins the Mastermind game
  else if (input == WIN) {
    // turn on all the LEDs first
    allLEDsOn();
    
    // wait a few seconds (1500 ms) before starting the show
    delay(1500);

    allLEDsOff();

    // 500 ms
    delay(500);
    
    // turn on the first yellow LED
    digitalWrite(allLEDs[0], HIGH);

    // 100 ms
    delay(100);

    // turn on the next pin and turn off the current pin, then wait
    for (int i = 1; i < 8; i++) {
      digitalWrite(allLEDs[i], HIGH);
      digitalWrite(allLEDs[i - 1], LOW);

      // 100 ms
      delay (100);
    }


    // now to go back (the above loop but reversed)
    for (int i = 7; i > 0; i--) {
      digitalWrite(allLEDs[i - 1], HIGH);
      digitalWrite(allLEDs[i], LOW);

      // 100 ms
      delay (100);
    }

    // turn on the first yellow LED
    digitalWrite(allLEDs[0], LOW);

    // 100 ms
    delay(100);

    for (int i = 0; i < 2; i++) {
      // turn on all LEDs
      allLEDsOn();

      // 200 ms
      delay (200);

      // turn off all LEDs
      allLEDsOff();

      // 300 ms
      delay (300);
    }
  }

  // when the Arduino is no longer being used by Mastermind, turn off the red LED to indicate that
  else if (input == DEACTIVATED) {
    digitalWrite(RED_PIN, LOW);
    allLEDsOff();
  }

  // if the input was a LED code
  else {
    // turn on the LEDs
    if (input == YELLOW0_CODE) {
      digitalWrite(YELLOW0_PIN, HIGH);
    }
    
    else if (input == YELLOW1_CODE) {
      digitalWrite(YELLOW1_PIN, HIGH);
    }
    
    else if (input == YELLOW2_CODE) {
      digitalWrite(YELLOW2_PIN, HIGH);
    }
    
    else if (input == YELLOW3_CODE) {
      digitalWrite(YELLOW3_PIN, HIGH);
    }
    
    else if (input == GREEN0_CODE) {
      digitalWrite(GREEN0_PIN, HIGH);
    }
    
    else if (input == GREEN1_CODE) {
      digitalWrite(GREEN1_PIN, HIGH);
    }
    
    else if (input == GREEN2_CODE) {
      digitalWrite(GREEN2_PIN, HIGH);
    }
    
    else if (input == GREEN3_CODE) {
      analogWrite(GREEN3_PIN, 100);
    }
  }
}

// this turns off all yellow and green LEDs
void allLEDsOff() {
  digitalWrite(YELLOW0_PIN, LOW);
  digitalWrite(YELLOW1_PIN, LOW);
  digitalWrite(YELLOW2_PIN, LOW);
  digitalWrite(YELLOW3_PIN, LOW);
  digitalWrite(GREEN0_PIN, LOW);
  digitalWrite(GREEN1_PIN, LOW);
  digitalWrite(GREEN2_PIN, LOW);
  digitalWrite(GREEN3_PIN, LOW);
}

// this turns on all yellow and green LEDs
void allLEDsOn() {
  digitalWrite(YELLOW0_PIN, HIGH);
  digitalWrite(YELLOW1_PIN, HIGH);
  digitalWrite(YELLOW2_PIN, HIGH);
  digitalWrite(YELLOW3_PIN, HIGH);
  digitalWrite(GREEN0_PIN, HIGH);
  digitalWrite(GREEN1_PIN, HIGH);
  digitalWrite(GREEN2_PIN, HIGH);
  analogWrite(GREEN3_PIN, 100);
}
