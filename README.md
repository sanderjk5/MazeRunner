# MazeRunner
Dieses Unity-Projekt beinhält alle notwendigen Dateien und Konfigurationen für die erste Version des MazeRunner Spiels.

Um das Spiel zu bauen, muss innerhalb von Unity der Bereich "Build Settings" aufgerufen werden und anschließend entweder Windows als Plattform oder WebGL als Option 
ausgewählt werden. 
Daraufhin werden über den Befehl "Build" die notwendigen Dateien und zusätzlich eine MazeRunner.exe bzw. index.html Datei über die das Spiel gestartet werden kann erzeugt.

Alternativ können die Zip Dateien in dem Ordner Releases gedownloadet werden, da diese bereits die Builds inklusive der ausführbaren Dateien enthalten.

Das Spiel beinhält bisher eine Szene in der die Spielfigur durch ein Labyrinth navigiert werden kann. Die Navigation erfolgt über die Pfeiltasten bzw. WASD.
Das Spiel wird automatisch beendet, wenn die Spielfigur das rote Quadrat erreicht hat. Das Labyrinth wird noch nicht zufällig erzeugt, da der Algorithmus, der die Wände erstellt, mit vorher festgelegten Kanten arbeitet.
