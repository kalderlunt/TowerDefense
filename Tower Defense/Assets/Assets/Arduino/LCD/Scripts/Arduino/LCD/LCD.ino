#include <LiquidCrystal.h>

const int rs = 12, en = 11, d4 = 5, d5 = 4, d6 = 3, d7 = 2;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

String inputString = "";

unsigned long lastSerialTime = 0;  // Dernière fois qu'un caractère a été reçu
const unsigned long waitTime = 2000; // en ms
bool readyDisplayed = false;
bool dataReceived = false;


void setup() 
{
  lcd.begin(16, 2);
  lcd.print("Ready!");
  Serial.begin(9600);
  
  readyDisplayed = true;
}

void loop() 
{
  dataReceived = false;

  while (Serial.available()) 
  {
    char c = Serial.read();
    
    lastSerialTime = millis(); // Met à jour le chrono à chaque caractère reçu
    dataReceived = true;

    if (c == '\n') // Fin de ligne
    {
      lcd.clear();
      lcd.setCursor(0, 0);
      lcd.print(inputString);
      Serial.println(inputString);
      inputString = "";
      readyDisplayed = false;
    }
    else 
    {
      inputString += c;
    }

  }
  
  if (!dataReceived && !readyDisplayed && millis() - lastSerialTime > waitTime)
  {
    lcd.clear();
    lcd.setCursor(0, 0);
    lcd.print("Ready!");
    readyDisplayed = true;
  }
}
