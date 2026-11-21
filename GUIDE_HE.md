# מדריך ביצוע והגשה – מטלה 3

## 1. סקירת מטלה

- **חלק א׳:** תכנון לפחות ארבע רמות (Tutorial, קלה, בינונית, קשה) לכל אחד משני המשחקים הקודמים. הוגדרו פתרונות קונקרטיים, מכשולים, פלטפורמות, משאבים ותרשים לכל רמה.
- **חלק ב׳:** מימוש רמה אחת ממשחק אחד; השתמשנו ב־Shadow Switch, רמה 2 (*האור שאוזל*).
- **ארבעת הנושאים המחייבים:** אובייקטי Prefab והולדה אוטומטית (Spawn), התנגשויות ב־2D, מעבר בין סצנות ושמירת נתונים, התאמה להפעלה בטלפון כולל WebGL.
- **נוסח המטלה:** בנוסף להפעלת WebGL מתוך הדפדפן במכשיר נייד, יש לבנות גרסת Android ולהקליט סרטון שמראה משחק בטלפון.

## 2. כיצד לפתוח את הפרויקט

1. פתחו Unity Hub, ובחרו **Add → Add project from disk**.
2. בחרו את התיקייה `PartB_ShadowSwitch_Unity` (לא את תיקיית המטלה הראשית).
3. מומלץ להתקין **Unity 2022.3 LTS**; אם הפרויקט נפתח בגרסה חדשה יותר, אשרו הסבה של הפרויקט אם Unity מציעה אותה. אין צורך להעתיק את קובצי C# ידנית.
4. ודאו ש־Unity Package Manager התקין את `Unity UI (com.unity.ugui)` ושב־Project Settings → Player → Active Input Handling נבחר `Input Manager (Old)` או `Both` (נדרש עבור קוד הקלט הנוכחי).
5. המתינו לסיום ייבוא הארט והקומפילציה; פתחו את התפריט העליון **Homework 3 → Generate Complete Game (Scenes + Prefabs)**. פעולה זו יוצרת את `Assets/Prefabs/*.prefab` ואת `Assets/Scenes/*.unity` ומוסיפה אותן ל־Build Settings.
6. פתחו `Assets/Scenes/MainMenu.unity`, לחצו **Play** ואז **PLAY LEVEL 2**.
7. **אם לא מופיע תפריט Homework 3:** חפשו שגיאות אדומות ב־Console ותקנו אותן לפני ההרצה; התפריט מופיע רק אחרי שהקוד מתקמפל.

## 3. איך משחקים

- `A`/`D` או חיצי שמאל/ימין – תנועה.
- `Space` / `W` – קפיצה.
- `J`/`L` או לחיצה עם העכבר לכיוון היעד – כיוון הפנס.
- `E` – הפעלה או כיבוי הפנס כשעומדים לידו.
- `R` – התחלה מחדש; `Esc` – תפריט ראשי.
- בטלפון יש לחצני כיוון, קפיצה, תאורה וסיבוב על המסך. מומלץ לשחק במצב לרוחב (Landscape).

**פתרון מוצע של הרמה:** מתחילים משמאל, מתקדמים ולוקחים גביש שמגדיל את האנרגיה, מתקרבים לפנס, מסובבים אותו לעבר האובליסק עד שהגשר מקבל צבע סגול אטום, מדליקים את הפנס, חוצים את הבור ומגיעים לדלת הטורקיז. כיבוי הפנס או התרוקנות הסוללה מעלימים את הגשר.

## 4. איפה כל דרישה ממומשת?

| דרישה | קבצים ועצמים בפרויקט | הסבר |
|---|---|---|
| Prefabs + Spawn | `Homework3ProjectBuilder.cs`, `PrefabSpawner.cs`, `MoteSpawner.cs`, `Assets/Prefabs/Crystal.prefab` | בונה Prefabs, יוצר גביש בתחילת משחק עם `Instantiate`, ומוליד חלקיקים בזמן שהגשר פעיל |
| נכסים שנוצרו בסיוע AI | `Assets/Art/Player.png`, `Lamp.png`, `Crystal.png`, `Obelisk.png` | גרפיקה שנוצרה במיוחד למטלה ומשמשת Prefabs ואובייקטים |
| התנגשויות | `PlayerController.cs`, `CrystalPickup.cs`, `ShadowBridge.cs`, `GoalTrigger.cs`, `PitTrigger.cs` | רצפה וגשר עם BoxCollider2D; איסוף/בור/יציאה עם Trigger |
| ניהול סצנות | `MainMenu`, `Level02`, `Victory`, `GameOver`; `GameSession.cs` | שימוש ב־SceneManager.LoadScene; `DontDestroyOnLoad` שומר אנרגיה, גבישים, ניסיונות וזמן |
| נייד + WebGL | `TouchControlButton.cs`, `MobileInputState.cs`, `Homework3BuildExporter.cs` | לחצנים נגישים במגע; יצוא WebGL ל־itch.io |
| Android | `Homework3BuildExporter.cs` | תפריט יצוא APK לאחר התקנת Android Build Support |

## 5. בניית WebGL והעלאה ל־itch.io

1. התקינו את רכיב **WebGL Build Support** ב־Unity Hub עבור הגרסה הרלוונטית.
2. כשכל הסצנות כבר נוצרו והמשחק עובד ב־Play, לחצו **Homework 3 → Export WebGL (for itch.io)**. לחלופין File → Build Settings → WebGL → Switch Platform → Build.
3. קובצי היצוא נוצרים בתיקייה `Builds/WebGL` **רק לאחר ש־Unity משלימה בנייה בהצלחה**.
4. דחסו ל־ZIP את **תוכן** התיקייה WebGL, כך שהקובץ `index.html` יהיה בשורש ה־ZIP.
5. היכנסו ל־itch.io → Dashboard → Create new project. בחרו **HTML** כסוג המשחק, העלו את קובץ ה־ZIP, וסמנו את האפשרות **This file will be played in the browser**. הפעילו את האפשרות שמתאימה לנייד, אם זמינה בפרויקט שלכם.
6. פתחו את הקישור בדפדפן בטלפון אמיתי ובדקו תנועה, קפיצה, תאורה, סיבוב, איסוף, ניצחון והפסד. מסכים צרים עשויים לדרוש שימוש לרוחב והגדלת סביבת המשחק.
7. העתיקו את כתובת ה־itch.io למסמך ההגשה, רק אחרי שווידאתם שהמשחק נטען.

## 6. בניית Android APK וסרטון

1. התקינו דרך Unity Hub את **Android Build Support** יחד עם **Android SDK & NDK Tools** ו־**OpenJDK**.
2. לחצו **Homework 3 → Export Android APK**, או השתמשו ב־Build Settings → Android.
3. קובץ `Builds/Android/ShadowSwitch.apk` יופיע רק אם תהליך ה־Build הצליח. הכנת APK עשויה לדרוש גם התאמה של Project Settings לגרסת המכשיר.
4. התקינו במכשיר Android שבבעלותכם, אפשרו התקנה ממקור אמין לפי הנחיות המכשיר והפעילו במצב לרוחב.
5. הקליטו מסך בטלפון תוך הדגמה של איסוף הגביש, יצירת גשר, מעבר, ומסך ניצחון/הפסד.
6. העלו את הסרטון ליעד ההגשה שביקש המרצה. **סרטון אמיתי ו־APK אינם נוצרים בתיקייה זו ללא הרצת Unity.**

## 7. מה להגיש בפועל

- קישור ל־GitHub הכולל `PartA_LevelDesign` ואת קובצי המקור של הפרויקט (עדיף לשים בתוך `homework3/`).
- תכנון ארבע הרמות לכל משחק כולל התרשימים.
- קישור ל־itch.io לאחר פרסום WebGL והוכחת בדיקת הדפדפן בנייד.
- אם נדרש, APK / סרטון ההתקנה והמשחק בטלפון.
- הסברים על חלקי הקוד והמכניקות (ראו `CODE_EXPLANATION_HE.md`).

**הערת הגינות טכנית:** אין כאן אישור לקומפילציה ב־Unity או להצלחת הבנייה, משום שהסביבה שבה נכתבו הקבצים אינה כוללת Unity Editor, רכיבי Android/WebGL או מכשיר נייד.
