# MazeRunner
Dieses Unity-Projekt beinhält alle notwendigen Dateien und Konfigurationen für die erste Version des MazeRunner Spiels.

Wir haben bereits die Algorithmen des Java Projekts übertragen und das Spiel um Menüs ergänzt. 
Dementsprechend wird das Labyrinth in zwei Größen inklusive einer beliebigen Anzahl an Hindernissen mit zugehörigen Buttons zufällig erzeugt. Dabei stehen jeweils die Buttons und Hindernisse mit der gleichen Farbe in einer Verbindung zueinander. Ziel ist es, die Spielfigur mit möglichst wenigen Schritten über die Pfeiltasten durch das Labyrinth zu navigieren. Das Überwinden eines vorhandenen Hindernisses kostet den Spieler im kleinen Labyrinth 25 und im großen 50 Schritte. Durch das Betätigen eines Buttons wird das zugehörige Hindernis entfernt. Es liegen jedoch nicht alle Hindernisse auf dem optimalem Pfad und teilweise ist es sogar notwendig ein Hindernis zu überwinden ohne den Button vorher zu betätigen. Dem Spieler wird am unteren Rand des Spiels seine aktuelle Schrittzahl und die für dieses Labyrinth optimale angezeigt.

Das Spiel beinhält zwei Spielmodi:
1. Im normalen Modus, der über "Play" gestartet werden kann, muss der Spieler seine gewünschte Schwierigkeit über den Slider auswählen. Anschließend kann er das Spiel über "Start" starten. Die Schwierigkeitsstufen skalieren über die Anzahl der Buttons und der Größe des Labyrinths. Sobald der Spieler das Labyrinth verlassen hat, kann er sich mit dem Button "Show optimal path" den Optimalen Pfad anzeigen lassen. Um daraufhin zu dem Endmenü zurück kehren zu können, muss er "ESC" drücken.
Dem Spieler wird während dem Spielen zusätzlich seine benötigte Zeit angezeigt, da sich seine Punktzahl (zwischen 0 und 100) nach der benötigten Zeit und der Abweichung der Schritte ableitet.

2. Im Level Modus, den der Spieler über "Level" im Hauptmenü auswählen kann, ist es das Ziel in 15 Minuten alle 8 Level des normalen Spielmodus in der Reihenfolge ihrer Schwierigkeit zu absolvieren. Dementsprechend wird der Spieler nach dem Verlassen eines Labyrinths direkt in das nächste Labyrinth gesetzt. Zusätzlich wird bei dem Verlassen eines Labyrinths seine Schrittdifferenz von der verbleibenden Zeit abgezogen.

In beiden Modi kann der Spieler während dem Spielen über das Betätigen des "ESC" Buttons ein Spielmenü aufrufen. Über dieses kann er zum Hauptmenü zurückkehren, das Spiel beenden oder das Spiel fortsetzen.

Das Spiel kann auf folgende unterschiedliche Arten ausgeführt werden:

1. Das Projekt muss zunächst in Unity geöffnet und die GameScene ausgewählt werden. Anschließend kann eine ausführbare Datei wie folgt erstellt werden:
    File --> Build Settings (gewünschte Plattform auswählen) --> Build and Run (dabei einen Ordner mit dem Namen "Build" erstellen und auswählen)
   Anschließend wird das Spiel gebaut und automatisch ausgeführt. In dem Ordner "Build" befindet sich zudem die ausführbare Datei, die ab sofort zum Spielstart verwendet werden kann.

2. Die Zip-Datei "WindowsBuild2101" oder "WebGLBuild2101" im Ordner "Releases" entpacken und die darin enthaltene "MazeRunner.exe" bzw. "index.html" Datei ausführen. Durch das Ausführen dieser wird auch das Spiel gestartet, da diese Ordner alle Dateien eines Builds enthalten. 
