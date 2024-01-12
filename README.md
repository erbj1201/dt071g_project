# dt071g_project
Källkod för slutprojekt i kursen DT071G, Programmering i C#.NET
Skapad av Erika Vestin, höstterminen 2023
erbj1201@student.miun.se

## Sänkaskepp - Battleship

Projetet är en konsolapplikation i form av ett spel skapat med C# och ramverket .NET. Spelet som skapast är ett sänkaskepp-spel där en spelare spelar mot datorn. Datorn och spelaren har varsin spelplan där skepp placeras ut randomiserat av applikationen.
Datorn och spelaren försöker sedan skjuta ner den andres skepp. Den som har skjutit ner motspelaren skepp först har vunnit.

### Hur startar man spelet 

En spelare startar applikationen och kommer till startsidan där det finns en navigering. För att starta ett spel behöver spelaren välja 1. För att starta ett spel. Därefter behöver användaren ange sitt namn och trycka på enter, om den inte anger ett namn tilldelas den namnet ”player_1”. För att starta spelet behöver spelaren sen trycka på enter och spelet startas. 

### Spelregler

Alla skepp för spelaren och datorn placeras ut av applikationen varierande, randomiserat på spelplanerna. Spelaren försöker träffa skepp på datorn spelplan och datorn försöker träffa skepp på spelaren spelplan. På datorns spelplan är alla rutor på spelplanen täckta med ett X för att spelaren inte ska kunna se vart skeppen är placerade. Spelaren skepp visas som blå S. När spelaren skjutit ett skott visas ett meddelande vilka koordinater som beskjutits och om det genererade en träff eller miss. Samma position kan aldrig beskjutas av datorn eller spelaren två gånger. När datorn skjuter ett skott visas ett meddelande vilka koordinater som beskjutits och om det var en träff eller en miss. En träff visas som ett rött H på spelplanen och en miss som ett gult M. Spelaren måste ange koordinater mellan noll och sex och koordinaterna kan inte lämnas tomma. Ett spel kan inte sparas och varje gång det är spelarens tur kan spelaren välja att avsluta spelet genom att trycka på z. När spelaren har skjutit behöver spelaren trycka på enter för att det ska bli datorns tur. När datorn skjutit behöver spelaren trycka på enter för att spelet få skjuta.

Spelet pågår till datorns eller spelaren alla skepp har blivit nedskjutna av motspelaren. Den som först skjutit ner motspelarens skepp vinner.

