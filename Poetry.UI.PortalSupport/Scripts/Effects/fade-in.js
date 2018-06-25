


/* FADE IN EFFECT */

class FadeInEffect {
    constructor(element, delay = 0) {
        var old = {
            opacity: element.style.opacity,
            transform: element.style.transform,
            transition: element.style.transition,
            zIndex: element.style.zIndex,
        };

        element.style.opacity = 0;
        element.style.transform = 'translate3d(-3rem, 0, 0)';
        element.style.zIndex = 0;

        setTimeout(() => {
            element.style.opacity = 1;
            element.style.transform = 'translate3d(0, 0, 0)';
            element.style.transition = 'transform 0.3s, opacity 0.3s';
        }, 1 + delay);

        setTimeout(() => {
            element.style.opacity = old.opacity;
            element.style.transform = old.transform;
            element.style.transition = old.transition;
            element.style.zIndex = old.zIndex;

            if (this.onCompleteCallback) {
                this.onCompleteCallback();
            }
        }, 300 + delay);

        return this;
    }

    onComplete(callback) {
        this.onCompleteCallback = callback;
    }
}

export default FadeInEffect;