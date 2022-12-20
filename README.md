# AnnoyedFiends - GDD

## Anleitung Prototyp
<li>Der Wavespawner startet automatisch damit, Gegner zu spawnen, sobald das Spiel gestartet wird</li>
<li>Die Gegner folgen einem fest vorgegebenen Pfad</li>
<li>Man wählt einen der 3 verfügbaren Türme mithilfe der Buttons an der linken Seite aus</li>
<li>Der ausgewählte Turm kann nun mit Linksklick überall auf dem Spielfeld plaziert werden, wo sich eine grüne Kachel befindet</li>
<li>Auf Feldern, wo sich bereits ein Turm befindet, oder die keine grüne Kachel haben, kann kein Turm plaziert werden</li>
<li>Sobald ein Gegner innerhalb der Schuss-Reichweite eines Turms ist, schießt der Turm auf diesen</li>
<li>Mit der rechten Maustaste auf einen Turm kann man diesen wieder vom Spielfeld entfernen</li>
<li>Erreichen die Gegner das Tor am Ende des Spielfeldes, werden diese despawned</li>


### Besonderheiten
<li>klickt man außerhalb des Spielfeldes, wird ein Tower unten links auf dem Feld plaziert</li>
<li>Standartmäßig ist der 1. Turm (Archer) ausgewählt, auch wenn man keinen Turm im UI angeklickt hat</li>
<li>Ist ein Turm über einen Button ausgewählt, kann man weiterhin diesen Turm plazieren, auch wenn der Button nicht erneut gedrückt wird</li>
<li>Türme schießen nur auf den nächsten Gegner, machen aber noch keinen Schaden</li>
<li>Die Gegner führen noch zu keinem GameOver, wenn sie das Ende der Karte erreichen</li>
<li>Das "Eye of Doom" in der Mitte der Karte hat noch keine Funktion</li>
<li>Das Spiel kann "nur" mit Alt+F4 beendet werden</li>


<br><br><br>

## Projektbeschreibung
<li>Kurze Zusammenfassung</li>
<li>Vergleich (z.B. Es spielt sich wie Temple Run)</li>
<li>Teile, die noch aktiv in Entwicklung sind (z.B. Die Story ist noch nicht konkret)</li>

## Character & Story
<li>Wichtige Charaktere + Beschreibung</li>
<li>Wichtige Story-Abschnitte + Wendungen</li>
<li>Theme (z.B. Steampunk, Post-Apokalypse, Cottage Core)</li>

## Gameplay
<details>
<summary>Game-Loop</summary>
Türme plazieren<br>
Geld verdienen durch Gegner töten<br>
Türme mit dem Geld verbessern und/oder neue Türme kaufen<br>
Es spawnen mehr und stärkere Gegner<br>
<li>Türme plazieren</li>
<li>Geld verdienen durch Gegner töten<br>
<li>Türme mit dem Geld verbessern und/oder neue Türme kaufen<br>
<li>Es spawnen mehr und stärkere Gegner<br>
</details>

<details>
<summary>Win & Lose</summary>
Win
<li>Story-Modus: Wenn man eine festen Anzahl an Runden überstanden hat, ohne das die Lebenspunkte auf 0 gesetzt sind, hat man die Karte gewonnen.</li>
<li>Endlos-Modus: Keine Win-Condition, nur Highscore-Jagd</li>
<br>
Lose
<li>Story- & Endlos-Modus: Wenn zu viele Gegner das Ende erreicht haben und die Lebenspunkte auf 0 gesunken sind.</li>
</details>

<details>
<summary>Interaktion / Skill</summary>
Taktische/strategische Plazierung der Türme<br>
Türme kaufen, verbessern, verkaufen<br>
Selbst aus dem Hauptturm schießen<br>
<li>Taktische/strategische Plazierung der Türme</li>
<li>Türme kaufen, verbessern, verkaufen</li>
<li>Selbst aus dem Hauptturm schießen</li>
</details>

<details>
<summary>Game-Mechanics</summary>
Zielpriorisierung der Türme
Türme kaufen
Türme plazieren
Türme verbessern
Karte im Story-Modus gewinnen, um sie im Endlos-Modus freizuschalten
<li>Zielpriorisierung der Türme</li>
<li>Türme kaufen</li>
<li>Türme plazieren</li>
<li>Türme verbessern</li>
<li>Karte im Story-Modus gewinnen, um sie im Endlos-Modus freizuschalten</li>
</details>

<details>
<summary>Tower</summary>
<li>Gargoyle</li>
<li>Archer</li>
<li>Teufel/Teufel Duo</li>
</details>
<details>
<summary>Enemies</summary>
<li>Bauern</li>
<li>Dorfschranzen</li>
<li>(Holzfäller)</li>
<li>Bauern</li>
</details>

<details>
**<summary>Progression & Herausforderung</summary>**
Spiel wird mit jeder Welle schwieriger<br>
Boss-Wellen<br>
(Schwierigkeitsmodus)<br>
<summary>Progression & Herausforderung</summary>
<li>Spiel wird mit jeder Welle schwieriger</li>
<li>Boss-Wellen</li>
<li>(Schwierigkeitsmodus)</li>
</details>


## Grafik Stil und Leveldesign
<li>Beschreibung “Look and Feel”</li>
<li>Konzept Art</li>
<li>Inspiration</li>

## Technische Beschreibung
<li>Wie in der Spiel-Analyse (RAM, Speicher...)</li>
