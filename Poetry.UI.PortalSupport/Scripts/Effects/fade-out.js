


/* FADE OUT EFFECT */

class FadeOutEffect {
    constructor(element, wait) {
        var value = 100;

        if (wait) {
            value += wait / 10;
        }
        
        var update = () => {
            value -= 10;

            if (value > 0) {
                element.style.opacity = Math.min(100, value) / 100;
                requestAnimationFrame(update);
            } else {
                if (this.onCompleteCallback) {
                    this.onCompleteCallback();
                }
            }
        };

        update();
    }

    onComplete(callback) {
        this.onCompleteCallback = callback;
    }
}