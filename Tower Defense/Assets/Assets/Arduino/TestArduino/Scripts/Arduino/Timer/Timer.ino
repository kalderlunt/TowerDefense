#include "SevSeg.h"
SevSeg sevseg;

// Variables globales
int Compteur = 0;     // début
int attente =0;        // compteur en attente
int attentemax =1000;  // définne la vitesse

void setup(){
  byte numDigits = 4;
  byte digitPins[] = {10, 11, 12, 13};
  byte segmentPins[] = {9, 2, 3, 5, 6, 8, 7, 4};

  bool resistorsOnSegments = true; 
  bool updateWithDelaysIn = true;
  byte hardwareConfig = COMMON_CATHODE; 
  sevseg.begin(hardwareConfig, numDigits, digitPins, segmentPins, resistorsOnSegments);
  sevseg.setBrightness(90);
}
void loop(){
  sevseg.refreshDisplay();
  sevseg.setNumber(Compteur, -1);
  // système pour augmenter le compteur et créer un système de vitesse
  attente = attente +1;
  if (attente == attentemax){
    attente =0;
    Compteur = Compteur + 1;
  }
}