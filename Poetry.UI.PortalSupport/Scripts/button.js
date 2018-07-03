


/* BUTTON */

class PortalButton {
    constructor(text, callback) {
        this.element = document.createElement('poetry-ui-portal-button');
        this.element.tabIndex = 0;
        this.element.innerText = text;

        this.element.addEventListener("keyup", event => {
            if (event.keyCode != 13) {
                return;
            }

            event.preventDefault();
            this.element.click();
        });

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

export default PortalButton;