<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <?php include "partials/head.php" ?>
</head>
<body>
    <div class="gameWindow hallway">
        <a href="index.php">
            <div class="goBackwards">
                <div class="caption goBackwards--text">- Zurück zum Menü -</div>
            </div>
        </a>
        <a href="kitchen.php">
            <div class="scaleDown cursorPointer hallwayToKitchen">
                <img src="assets/objekts/door-left.png" />
            </div>
        </a>
        <a href="livingroom.php">
            <div class="scaleDown cursorPointer hallwayToLivingroom">
                <img src="assets/objekts/door-mid.png" />
            </div>
        </a>
        <a href="dorm.php">
            <div class="scaleDown cursorPointer hallwayToDorm">
                <img src="assets/objekts/door-right.png" />
            </div>
        </a>
    </div>
</body>

</html>