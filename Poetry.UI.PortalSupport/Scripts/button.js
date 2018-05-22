


/* BUTTON */

class PortalButton {
    constructor(text, callback) {
        this.element = document.createElement('portal-button');
        this.element.tabIndex = 0;
        this.element.innerText = text;

        if (callback) {
            this.element.addEventListener('click', callback);
        }
    }

    appendTo(element) {
        element.appendChild(this.element);

        return this;
    }

    setText(text) {
        this.element.innerText = text;
    }
}