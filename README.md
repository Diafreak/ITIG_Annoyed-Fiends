# AnnoyedFiends - GDD

## Anleitung Prototyp
- Der Wavespawner startet automatisch damit, Gegner zu spawnen, sobald das Spiel gestartet wird
- Die Gegner folgen einem fest vorgegebenen Pfad
- Man wählt einen der 3 verfügbaren Türme mithilfe der Buttons an der linken Seite aus
- Der ausgewählte Turm kann nun mit Linksklick überall auf dem Spielfeld plaziert werden, wo sich eine grüne Kachel befindet
- Auf Feldern, wo sich bereits ein Turm befindet, oder die keine grüne Kachel haben, kann kein Turm plaziert werden
- Sobald ein Gegner innerhalb der Schuss-Reichweite eines Turms ist, schießt der Turm auf diesen
- Mit der rechten Maustaste auf einen Turm kann man diesen wieder vom Spielfeld entfernen
- Erreichen die Gegner das Tor am Ende des Spielfeldes, werden diese despawned


### Besonderheiten
- klickt man außerhalb des Spielfeldes, wird ein Tower unten links auf dem Feld plaziert
- Standartmäßig ist der 1. Turm (Archer) ausgewählt, auch wenn man keinen Turm im UI angeklickt hat
- Ist ein Turm über einen Button ausgewählt, kann man weiterhin diesen Turm plazieren, auch wenn der Button nicht erneut gedrückt wird
- Türme schießen nur auf den nächsten Gegner, machen aber noch keinen Schaden
- Die Gegner führen noch zu keinem GameOver, wenn sie das Ende der Karte erreichen
- Das "Eye of Doom" in der Mitte der Karte hat noch keine Funktion
- Das Spiel kann "nur" mit Alt+F4 beendet werden


<br><br><br>

## Projektbeschreibung
- Kurze Zusammenfassung
- Vergleich (z.B. Es spielt sich wie Temple Run)
- Teile, die noch aktiv in Entwicklung sind (z.B. Die Story ist noch nicht konkret)

## Character & Story
- Wichtige Charaktere + Beschreibung
- Wichtige Story-Abschnitte + Wendungen
- Theme (z.B. Steampunk, Post-Apokalypse, Cottage Core)

## Gameplay
<details>
<summary>Game-Loop</summary>
Türme plazieren<br>
Geld verdienen durch Gegner töten<br>
Türme mit dem Geld verbessern und/oder neue Türme kaufen<br>
Es spawnen mehr und stärkere Gegner<br>
- Türme plazieren
- Geld verdienen durch Gegner töten<br>
- Türme mit dem Geld verbessern und/oder neue Türme kaufen<br>
- Es spawnen mehr und stärkere Gegner<br>
</details>

<details>
<summary>Win & Lose</summary>
Win
- Story-Modus: Wenn man eine festen Anzahl an Runden überstanden hat, ohne das die Lebenspunkte auf 0 gesetzt sind, hat man die Karte gewonnen.
- Endlos-Modus: Keine Win-Condition, nur Highscore-Jagd
<br>
Lose
- Story- & Endlos-Modus: Wenn zu viele Gegner das Ende erreicht haben und die Lebenspunkte auf 0 gesunken sind.
</details>

<details>
<summary>Interaktion / Skill</summary>
Taktische/strategische Plazierung der Türme<br>
Türme kaufen, verbessern, verkaufen<br>
Selbst aus dem Hauptturm schießen<br>
- Taktische/strategische Plazierung der Türme
- Türme kaufen, verbessern, verkaufen
- Selbst aus dem Hauptturm schießen
</details>

<details>
<summary>Game-Mechanics</summary>
Zielpriorisierung der Türme
Türme kaufen
Türme plazieren
Türme verbessern
Karte im Story-Modus gewinnen, um sie im Endlos-Modus freizuschalten
- Zielpriorisierung der Türme
- Türme kaufen
- Türme plazieren
- Türme verbessern
- Karte im Story-Modus gewinnen, um sie im Endlos-Modus freizuschalten
</details>

<details>
<summary>Tower</summary>
- Gargoyle
- Archer
- Teufel/Teufel Duo
</details>
<details>
<summary>Enemies</summary>
- Bauern
- Dorfschranzen
- (Holzfäller)
- Bauern
</details>

<details>
**<summary>Progression & Herausforderung</summary>**
Spiel wird mit jeder Welle schwieriger<br>
Boss-Wellen<br>
(Schwierigkeitsmodus)<br>
<summary>Progression & Herausforderung</summary>
- Spiel wird mit jeder Welle schwieriger
- Boss-Wellen
- (Schwierigkeitsmodus)
</details>


## Grafik Stil und Leveldesign
- Beschreibung “Look and Feel”
- Konzept Art
- Inspiration

## Technische Beschreibung
- Wie in der Spiel-Analyse (RAM, Speicher...)
