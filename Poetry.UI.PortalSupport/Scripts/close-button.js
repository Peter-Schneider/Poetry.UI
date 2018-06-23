


/* CLOSE BUTTON */

class BladeCloseButton {
    constructor(app, blade, callback) {
        this.element = document.createElement('blade-title-close');
        this.element.setAttribute('tabindex', 0);
        this.element.addEventListener('click', callback || (() => app.closeBlade(blade)));
    }
}

export default BladeCloseButton;