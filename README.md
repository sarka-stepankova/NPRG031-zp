# Hra PacMan

Zápočtový program vytvořený pro předmět Programování 2 v C#, LS 2021/22. Autorem programu je Šárka Štěpánková. Jedná se o napodobení hry PacMan vytvořené ve Windows Forms.


## Uživatelská část

### Návod ke spuštění

Hru lze spustit poklikáním na soubor `PacMan.exe` v hlavním adresáři. Pokud si budete chtít `PacMan.exe` zkopírovat jinam, je k němu vždy potřeba přiložit i soubor `board.txt`, který slouží pro načítání bludiště, ve kterém se PacMan pohybuje. Další možností je spuštění hry jako projekt, který je ve složce [Pacman](student-stepans1/Pacman/), ve Visual Studiu.

### Ovládání hry

Po spuštění hry se zobrazí obrazovka menu. Obrazovku menu ovládejte pomocí myši. Stačí jednoduché kliknutí na *PLAY GAME*, po kterém se dostanete na obrazovku hry s bludištěm. Hru odstartujete stisknutím klávesy šipky doleva ← nebo doprava →.

Pro ovládání PacMana (žluté nekonvexní kruhové výseče) jsou určené klávesy šipek **←, ↑, ↓, →**.

### Pravidla hry PacMan

Hra začíná v okamžiku, kdy uživatel stiskne klávesu šipek **←, →**. PacMan se začne pohybovat zvoleným směrem. Cílem hry je navést PacMana po obrazovce podobné bludišti tak, aby zkonzumoval *všechny žluté penízky*. Těch je 150, a za každý sebraný získá PacMan 1 bod do celkového Score. Při sbírání se ale musí vyhýbat duchům.

Duchové mají čtyři různé barvy: Blinky (červený), Pinky (růžový), Inky (azurový), Clyde (oranžový). Každý duch má jinou strategii útoku, např. Inky útočí přímo na Pacmana, ale když je příliš blízko něj, je zmaten a začne se pohybovat náhodně. Detailní implementace strategií duchů je popsána v programátorské části.

Jak hra postupuje, duchové opouštějí oblast kolem svého domečku („klece duchů“) a začnou se potulovat po hrací ploše. Pokud se PacMan srazí s duchem, přijde o život a hra se restartuje. Náš hlavní hrdina má ale 3 životy a dokud je nevyplýtvá, může ve hře pokračovat dál.

V rozích bludiště jsou k dispozici čtyři žetony (Power pelets). Pokud PacMan jeden z nich spolkne, všichni duchové se zbarví do tmavě modra a on je může sežrat. Jakmile je duch pohlcen, zmizí a jeho oči běží zpět k domečku. Tam se obnoví jeho ztracená síla a může zase začít honit Pacmana. Za sežrání ducha PacMan získá do celkového Score 10 bodů navíc.


## Programátorská část

### Popis struktury programu a vybraných funkcí

Celý projekt se nachází ve složce Pacman a obsahuje tři hlavní soubory. V souboru `Program.cs` se vytváří formulář hry. Kód k němu je uložený v `GameForm.cs`. Implementace tříd se nachází v souboru `Game.cs`.

Pro hru je klíčové načtení bludiště, které je reprezentované jako textový soubor `board.txt`. Skládá se ze znaků `B` (brick - překážka), `C` (coin), `T` (token), ` ` (prázdné políčko). PacMan a duchové jsou jako objekty umisťovány na mapu separátně, proto se v souboru nenachází. 

Výstupem programu je obrazovka na kterou je vykresleno bludiště s PacManem. Překreslování obrazovky probíhá ve funkci `GameForm_Paint()` pokaždé, když se pohyblivé objekty někam přemístí. Každý objekt má pro tuto příležitost určenou metodu `redrawObjekt()`.

Pohyb a změna stavu objektů probíhá v `mainTimer_Tick()`. Pokaždé, když se funkce zavolá, Pacman a duchové se pohnou o jedno políčko nahoru, dolu, doleva, nebo doprava. Protože jsou pohyby řešeny diskrétně, posouvají se pohyblivé objekty ve hře trhaně.

### Popis tříd Map a Pacman

#### Třída `Map`

Třída slouží k reprezentaci bludiště. V proměnných `widthCount` a `heightCount` je uložená informace o počtu políček v bludišti na šířku a na výšku. Velikost jednotlivých políček je uložená v `rectHeight` a `rectWidth`. Konstruktor pomocí metody `loadMap()` načte bludiště do proměnné `board`. 

#### Třída `Pacman`

Tato třída je určená pro reprezentaci pohyblivého objektu, PacMana. Nese v sobě informaci o jeho pozici (`x`, `y`), směru a o skóre. Proměná `coins` odpočítává kolik ještě zbývá sebrat žlutých penízků. Objekt v sobě navíc obsahuje i odkaz na bludiště (`map`), aby věděl na kterou pozici v bludišti může šlápnout a jestli tam, kam se přemístil, je penízek nebo token. Metoda `movePacman()` slouží k rozhodování, kam udělá Pacman další krok a k jeho přemístění (upravení `x`, `y`) podle směru určeného stisknutou šipkou, nebo dosavadním směrem, pokud ve směru stisknuté šipky krok udělat nemůže.

### Abstraktní třída Ghost a její potomci

Abstraktní třída Ghost slouží jako šablona pro všechny duchy v bludišti. Má v sobě informaci o pozici a směru, stejně jako třída Pacman. Navíc nese informaci o stavu ducha (`state`), ve kterém je uložená jedna z hodnot `enum GhostState`. Duch se totiž může nacházet ve stavu **chase**, kdy honí PacMana, **frightened**, to je vystrašený a PacMan ho může sežrat a **eaten**, kdy běží zpátky do domečku. Duch se *nemůže otočit o 180°*. Smí to udělat pouze při změně stavu. Dál v sobě třída obsahuje `counter` pro odpočítávání délky frightened stavu, `targetX` a `targetY` pro určení místa kam se duch má pohybovat a nakonec `prevX` a `prevY` jsou hodnoty, odkud duch přišel na nové políčko a slouží především pro zjištění, jestli se v jednom tahu s Pacmanem nevyměnil, tedy někdo někoho nesežral.

Metoda `redrawGhost()` je v tomto případě abstraktní, kvůli různé barvě duchů, vykreslující se na obrazovku. Metoda `findDistanceBetween2()` najde vzdálenost mezi dvěma zadanými body vzdušnou čarou. S abstraktní metodou `findTargetByState()` slouží metodě `moveGhost()` pro určování dalšího kroku ducha. Pokud jsou potenciální tahy ducha stejně vzdálené od cíle, pak duch dává přednost vydat se nahoru, pak doleva, dolů a doprava. Abstraktní metoda `goBackHome()` slouží k umístění ducha na startovní pozici při restartu hry.

#### Potomek Blinky

Jedná se o červeného ducha. Metoda `findTargetByState()` určuje jeho cíl. V případě `chase` stavu má nastavený cíl přímo na aktuální pozici Pacmana. V případě vystrašení (`frightened` stavu) se pohybuje náhodně, stejně jako ostatní druhy duchů a pokud je sněden, putuje jako ostatní druhy k domečku. Blinky začne honit PacMana okamžitě po spuštění hry.

#### Potomek Pinky

Tento duch je růžový. Jeho cílem při `chase` stavu jsou 4 políčka před PacManem, pokud jde PacMan doleva, doprava, nebo dolů. Pokud jde nahoru, pak je cíl ducha 4 políčka před Pacmanem a 4 doleva. Zatímco Blinky má tendenci honit PacMana zezadu, Pinky ho nahání spíš zepředu a tvoří tak s Blinkym silnou dvojici. Pinky začne honit Pacmana až potom, co sežere 10 penízků. Předtím se motá kolem domečku.

#### Potomek Inky

Barva tohoto ducha je azurová. Jeho cílem, stejně jako u Blinkyho, je PacMan, ale když je od něj vzdálený méně, než na 8 kroků, začne se pohybovat náhodně. Začíná honit až když PacMan sežere 15 penízků.

#### Potomek Clyde

Tento duch je oranžový. Jak už jméno naznačuje, vybočuje od ostatních duchů. Po celou dobu hry se v `chase` stavu pohybuje náhodně a PacMana nehoní. Od domečku se rozuteče potom, co sní PacMan 20 penízků.


## Zásluhy

Stategie duchů je v poupravené formě převzata z tohoto videa: [Pac-Man Ghost AI Explained](https://www.youtube.com/watch?v=ataGotQ7ir8).
