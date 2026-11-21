# הסבר קוד – Shadow Switch

## מבנה הפרויקט

`Assets/Editor/Homework3ProjectBuilder.cs` הוא כלי עריכה שנפתח מתפריט Unity. הוא מגדיר את התמונות כ־Sprites, יוצר ארבעה Prefabs (Player, Lamp, Crystal, LightMote), מקים את סצנת הרמה ואת מסכי התפריט/הניצחון/ההפסד, ומוסיף את הסצנות ל־Build Settings.

`Assets/Editor/Homework3BuildExporter.cs` מפעיל את Unity Build Pipeline עבור WebGL או Android אחרי שהסצנות קיימות.

## איך מתבצע המשחק?

1. **תנועה וקפיצה:** ב־`PlayerController.Update()` מתבצעת קריאת הקפיצה ונבדקת קרקע בעזרת `Physics2D.OverlapBoxAll`. ב־`FixedUpdate()` מעדכנים את מהירות התנועה של `Rigidbody2D`. בלא מגע ברצפה או בגשר, כוח הכבידה גורם לנפילה.
2. **הפנס:** `LampController` מחזיק זווית כיוון, אנרגיה ומצב דלוק/כבוי. בכל פריים פנס דולק צורך `consumptionPerSecond * Time.deltaTime`. כדי להפעיל גשר עליו להיות דלוק, בעל אנרגיה, ומכוון בזווית המתאימה לאובליסק הייעודי.
3. **הגשר:** `ShadowBridge.Update()` מפעיל או מבטל `BoxCollider2D` בהתאם ל־`LampController.BridgeConditionsMet`, ומשנה את שקיפות הגרפיקה. צל רגיל אינו משטח, רק גשר B1 שתוכנן מראש.
4. **Spawn:** `PrefabSpawner.Start()` קורא `Instantiate(prefab, ...)` עבור גביש אחד, ו־`MoteSpawner` יוצר חלקיקים חוזרים מעל הגשר כאשר הוא פעיל. החלקיקים נהרסים ב־`MoteLife`.
5. **איסוף גביש:** `CrystalPickup.OnTriggerEnter2D` בודק שהנוגע הוא PlayerController; מוסיף אנרגיה לפנס, מעלה את מונה הגבישים ומוחק את הגביש.
6. **כישלון וניצחון:** `PitTrigger` מזהה נפילה לבור ופותח `GameOver`. `GoalTrigger` מזהה מעבר בדלת ופותח `Victory`.
7. **שמירת נתונים:** `GameSession` נוצר פעם אחת, עם `DontDestroyOnLoad`. לפני טעינת מסך תוצאה הוא שומר גבישים, אנרגיה, ניסיונות וזמן. כאשר חוזרים לרמה הוא מאפס רק את נתוני הניסיון הנוכחי.
8. **טלפון:** `TouchControlButton` מממש `IPointerDownHandler`/`IPointerUpHandler`. התנועה והסיבוב רציפים כל עוד הלחצן לחוץ; קפיצה והדלקה הן אירועים חד־פעמיים שנאספים ב־`MobileInputState`.

## שאלות בדיקה שאפשר לשאול על הקוד

- **למה משתמשים ב־Prefab?** אובייקט שאפשר לשכפל באופן אחיד עם `Instantiate`, ולשנות במקור במקום לעדכן עותקים באופן ידני.
- **מה ההבדל בין `Collider2D` ל־`isTrigger`?** קוליידר רגיל חוסם תנועה פיזית; Trigger מזהה מעבר או חפיפה ויורה אירוע בלי לחסום.
- **למה `FixedUpdate` לתנועה?** כי תזמון הפיזיקה קבוע; קלטים חד־פעמיים נקלטים ב־`Update`, ואז מיושמים בשלב פיזיקה.
- **למה `Time.deltaTime` לצריכת אנרגיה?** כדי שהאנרגיה תרד בקצב דומה בשנייה בלי להיות תלויה בקצב הפריימים.
- **איך נמנעת פתיחת שער כשפנס כבוי?** `BridgeConditionsMet` מחזיר `false` כשהפנס אינו דולק, אין אנרגיה או הזווית שגויה.
- **מה נשמר כשמחליפים סצנה?** האובייקט `GameSession` ושדותיו, באמצעות `DontDestroyOnLoad`.
- **איך הטלפון שולח תנועה?** כפתורי המגע מעדכנים מצבי לחיצה סטטיים; `PlayerController` קורא אותם כמו מקשי מקלדת.

> ההסבר מתייחס לקוד שנכתב בחבילה זו ולא לתוצאות של בדיקות שנעשו בפועל במנוע.
