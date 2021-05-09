# MazeRunner
Dieses Unity-Projekt beinhält alle notwendigen Dateien und Konfigurationen für die erste Version des MazeRunner Spiels.

Wir haben bereits die Algorithmen des Java Projekts übertragen und das Spiel um Menüs ergänzt. 
Dementsprechend wird das Labyrinth in zwei Größen inklusive einer beliebigen Anzahl an Hindernissen mit zugehörigen Buttons zufällig erzeugt. Dabei stehen jeweils die Buttons und Hindernisse mit der gleichen Farbe in einer Verbindung zueinander. Ziel ist es, die Spielfigur mit möglichst wenigen Schritten über die Pfeiltasten durch das Labyrinth zu navigieren. Das Überwinden eines vorhandenen Hindernisses kostet den Spieler im kleinen Labyrinth 25 und im großen 50 Schritte. Durch das Betätigen eines Buttons wird das zugehörige Hindernis entfernt. Es liegen jedoch nicht alle Hindernisse auf dem optimalem Pfad und teilweise ist es sogar notwendig ein Hindernis zu überwinden ohne den Button vorher zu betätigen. Dem Spieler wird am unteren Rand des Spiels seine aktuelle Schrittzahl und die für dieses Labyrinth optimale angezeigt.

Das Spiel beinhält zwei Spielmodi:
1. Im normalen Modus, den der Spieler über "Common" im Hauptmenü auswählen kann, muss der Spieler seine gewünschte Schwierigkeit über den Slider auswählen. Anschließend kann er das Spiel über "Start" starten. Die Schwierigkeitsstufen skalieren über die Anzahl der Buttons und der Größe des Labyrinths. Sobald der Spieler das Labyrinth verlassen hat, kann er sich mit dem Button "Show optimal path" den Optimalen Pfad anzeigen lassen. Um daraufhin zu dem Endmenü zurück kehren zu können, muss er "ESC" drücken.
Dem Spieler wird während dem Spielen zusätzlich seine benötigte Zeit angezeigt, da sich seine Punktzahl (zwischen 0 und 100) nach der benötigten Zeit und der Abweichung der Schritte ableitet. Für jede Schwierigkeitsstufe existiert ein Highscore, der die höchste Punktzahl des Spielers in dieser anzeigt.

2. Im Level Modus, den der Spieler über "Level" im Hauptmenü auswählen kann, ist es das Ziel in einer bestimmten Zeit alle 8 Level des normalen Spielmodus in der Reihenfolge ihrer Schwierigkeit zu absolvieren. Dementsprechend wird der Spieler nach dem Verlassen eines Labyrinths direkt in das nächste Labyrinth gesetzt. Nach dem Verlassen eines Levels erhält der Spieler einen Zeitbonus, der sich nach der Abweichung der Schrittzahl von der optimalen des vorangegangenen Levels richtet. Die Leistung des Spielers wird als Schriftzug angezeigt. Es existiert zudem ein Highscore der aus dem höchsten erreichten Level und der maximal übrig gebliebenen Zeit nach dem Beenden des letzten Levels besteht.

3. Im Battle Modus, den der Spieler über "Battle" im Hauptmenü auswählen kann, muss der Spieler gegen einen Gegner antreten. Dabei muss der Spieler das Labyrinth in der rechten unteren Ecke und sein Gegner über die linke untere Ecke verlassen. Am Ende gewinnt der Spieler, der eine höhere Punktzahl erzielt hat. Die Punktzahl setzt sich aus der Schrittanzahl und benötigter Zeit zusammen. Im Labyrinth befinden sich acht hellblaue Rauten, die von Spieler und Gegner betätigt werden könnnen. Bei der Betätigung wird entweder ein Hindernis inklusive Button auf dem optimalen Pfad des jeweiligen anderen platziert oder der andere kann sich für 10 Sekunden nicht bewegen. Dabei können maximal vier Hindernisse hinzugefügt werden und die Art des Resultats der Betätigung der Rauten erfolgt zufällig.

In allen drei Modi kann der Spieler während dem Spielen über das Betätigen des "ESC" Buttons ein Spielmenü aufrufen. Über dieses kann er zum Hauptmenü zurückkehren, das Spiel beenden oder das Spiel fortsetzen.

Das Spiel kann auf folgende unterschiedliche Arten ausgeführt werden:

1. Das Projekt muss zunächst in Unity geöffnet und die GameScene ausgewählt werden. Anschließend kann eine ausführbare Datei wie folgt erstellt werden:
    File --> Build Settings (gewünschte Plattform auswählen) --> Build and Run (dabei einen Ordner mit dem Namen "Build" erstellen und auswählen)
   Anschließend wird das Spiel gebaut und automatisch ausgeführt. In dem Ordner "Build" befindet sich zudem die ausführbare Datei, die ab sofort zum Spielstart verwendet werden kann.

2. Die Zip-Datei "WindowsBuild1904" im Ordner "Releases/3_1904" entpacken und die darin enthaltene "MazeRunner.exe" Datei ausführen. Durch das Ausführen dieser wird auch das Spiel gestartet, da dieser Ordner alle Dateien eines Builds enthält. 
