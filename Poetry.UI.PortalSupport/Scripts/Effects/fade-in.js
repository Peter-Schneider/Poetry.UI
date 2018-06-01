


/* FADE IN EFFECT */

class FadeInEffect {
    constructor(element) {
        var value = 0;

        return new Promise(callback => {
            function update() {
                if (value++ < 10) {
                    element.style.opacity = value / 10;
                    requestAnimationFrame(update);
                } else {
                    callback();
                }
            }

            update();
        });
    }
}