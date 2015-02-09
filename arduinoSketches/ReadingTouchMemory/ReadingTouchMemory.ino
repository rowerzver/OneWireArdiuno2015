#include <OneWire.h>

OneWire  ds(10);

void setup(void) {
  Serial.begin(9600);
}

void loop(){
  byte key[8];
  
   if(ds.search(key)){
     for(int i=0; i<8; i++){
      Serial.println(key[i], HEX); 
     }
     Serial.println("\n\n");
   }
}

//
//1
//FF
//FF
//FF
//FF
//FF
//FF
//2F
//


//1
//0
//0
//0
//0
//0
//0
//3D


