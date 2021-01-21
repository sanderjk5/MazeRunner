# MazeRunner
Dieses Unity-Projekt beinhält alle notwendigen Dateien und Konfigurationen für die erste Version des MazeRunner Spiels.

Wir haben bereits die Algorithmen des Java Projekts übertragen. 
Dementsprechend wird das Labyrinth inklusive dreier Hindernisse mit zugehörigen Buttons zufällig erzeugt. Dabei stehen jeweils die Buttons und Hindernisse mit der gleichen Farbe in einer Verbindung zueinander. Ziel ist es, die Spielfigur mit möglichst wenigen Schritten über die Pfeiltasten durch das Labyrinth zu navigieren. Das Überwinden eines vorhandenen Hindernisses kostet den Spieler 25 Schritte. Durch das Betätigen eines Buttons wird das zugehörige Hindernis entfernt. Es liegen jedoch nicht alle Hindernisse auf dem optimalem Pfad und teilweise ist es sogar notwendig ein Hindernis zu überwinden ohne den Button vorher zu betätigen. Dem Spieler wird am unteren Rand des Spiels seine aktuelle Schrittzahl und die für dieses Labyrinth optimale angezeigt. Das Spiel endet sobald der Spieler das Labyrinth verlassen hat.

Das Spiel kann auf folgende unterschiedliche Arten ausgeführt werden:

1. Das Projekt muss zunächst in Unity geöffnet und die GameScene ausgewählt werden. Anschließend kann eine ausführbare Datei wie folgt erstellt werden:
    File --> Build Settings (gewünschte Plattform auswählen) --> Build and Run (dabei einen Ordner mit dem Namen "Build" erstellen und auswählen)
   Anschließend wird das Spiel gebaut und automatisch ausgeführt. In dem Ordner "Build" befindet sich zudem die ausführbare Datei, die ab sofort zum Spielstart verwendet werden kann.

2. Die Zip-Datei "WindowsBuild2101" oder "WebGLBuild2101" im Ordner "Releases" entpacken und die darin enthaltene "MazeRunner.exe" bzw. "index.html" Datei ausführen. Durch das Ausführen dieser wird auch das Spiel gestartet, da diese Ordner alle Dateien eines Builds enthalten. 

3. Das Spiel direkt in Unity über den Play Button starten. Jedoch wird es dann nicht in Vollbild ausgeführt und das Spiel nicht nach dem Verlassen des Labyrinths beendet.
