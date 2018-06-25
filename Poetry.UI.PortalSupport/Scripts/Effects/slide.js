


/* SLIDE EFFECT */

class SlideEffect {
    constructor(element, from, to, delay = 0) {
        var old = {
            width: element.style.width,
            transition: element.style.transition,
        };

        element.style.width = from;

        setTimeout(() => {
            element.style.width = to;
            element.style.transition = 'width 0.3s';
        }, 1 + delay);

        setTimeout(() => {
            element.style.width = old.width;
            element.style.transition = old.transition;

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

export default SlideEffect;