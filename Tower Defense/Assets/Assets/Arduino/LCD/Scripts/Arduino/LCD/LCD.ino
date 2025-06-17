#include <LiquidCrystal.h>

const int rs = 12, en = 11, d4 = 5, d5 = 4, d6 = 3, d7 = 2;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

String inputString = "";

void setup() {
  lcd.begin(16, 2);
  lcd.print("Ready!");
  Serial.begin(9600);
}

void loop() {
  while (Serial.available()) {
    char c = Serial.read();
    if (c == '\n') { // Fin de ligne reçue
      lcd.clear();
      lcd.setCursor(0, 0);
      lcd.print(inputString);
      inputString = "";
    } else {
      inputString += c;
    }
  }
}
