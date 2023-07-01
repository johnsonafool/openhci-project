#define trigPin1 9
#define echoPin1 8
#define trigPin2 12
#define echoPin2 11
#define threshold1 8
#define threshold2 8
long duration, distance;

void setup() {
  Serial.begin (9600);
  pinMode(trigPin1, OUTPUT);
  pinMode(echoPin1, INPUT);
  pinMode(trigPin2, OUTPUT);
  pinMode(echoPin2, INPUT);
}
 
void loop()
{
  SonarSensor(trigPin1, echoPin1);

  if (distance < threshold1){
    Serial.println("close1");
  } else {
    Serial.println("far1");
  }

  SonarSensor(trigPin2,echoPin2);

  if (distance < threshold2){
    Serial.println("close2");
  } else {
    Serial.println("far2");
  }
  
  delay(1000);
}

void SonarSensor(int trigPinSensor,int echoPinSensor)
{
  digitalWrite(trigPinSensor, LOW);
  delayMicroseconds(5);
  digitalWrite(trigPinSensor, HIGH);
  delayMicroseconds(10); 
  digitalWrite(trigPinSensor, LOW);

  duration = pulseIn(echoPinSensor, HIGH);
  distance = (duration/2) / 29.1;
}
