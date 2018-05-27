


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

    addClass(value, test) {
        if (test) {
            this.element.classList.add(value);
        } else {
            this.element.classList.remove(value);
        }

        return this;
    }

    appendTo(element) {
        element.appendChild(this.element);

        return this;
    }
}