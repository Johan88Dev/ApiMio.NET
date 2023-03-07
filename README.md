# WebApiMioExercise

Efter att klonen gjorts och programmet öppnas i Visual Studio 2022, så behövs en lokal SQL db initieras. <br>
1: Öppna <b>Package Manager</b> Console via tools -> nuget package manager<br>
2: Skriv: <b>add-migration initial</b><br>
3: När build är klar, skriv: <b>Update-Database</b><br>
4: När det är klart öppna <b>Custom SQL</b> via solution explorer -> dbContent<br>
5: Kopiera nu hela sökvägen till db.json i dbContent mappen, klistra in på de två rödmarkerade områdena i custom SQL filen (Rad 11 & Rad 38)<br>
6: Kör SQL koden<br>
7: Vid prompt, välj lokal och ange Database Name som är ProductsDb  (finns i dropdownen)<br>
8: Nu bör databasen vara fylld med produktdata.<br>
<br>


9: APIn bör nu fungerar och leverera data till <a href="https://github.com/Johan88Dev/Mio-React">React applikationen</a> i frontend uppgiften<br>
10: 2 endpoints finns, alla produkter och individuella produkter<br>
11: om en endpoint som inte finns efterfrågas så leveras ett standard 404 response via swagger <br>
12: Man kan via swagger utföra alla CRUD metoder<br>
<br>
Den stora skillnaden på denna API-strukturen och den db.json fil som följde med uppgiften, är att campaign inte längre är en nästlad JSON
utan den har istället plattats ut genom att lyfta ut 'name' och 'discountPercent' ur campaign och istället lägga dom i en rak json struktur.
