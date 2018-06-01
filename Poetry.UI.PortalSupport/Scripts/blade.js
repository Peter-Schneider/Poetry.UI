


/* BLADE */

class Blade {
    constructor(fullscreen) {
        this.element = document.createElement('blade');

        if (fullscreen) {
            this.element.classList.add('blade-fullscreen');
        }
    }

    setTitle(text) {
        if (this.title) {
            while (this.title.firstChild) {
                this.title.removeChild(this.title.firstChild);
            }
        } else {
            this.title = document.createElement('blade-title');

            this.titleText = document.createElement('blade-title-text');
            this.title.appendChild(this.titleText);

            if (this.closeButton) {
                this.title.appendChild(this.closeButton);
            }

            this.element.appendChild(this.title);
        }

        if (text) {
            this.title.style.display = '';
        } else {
            this.title.style.display = 'none';
        }

        this.titleText.innerText = text;
    }

    setToolbar(...items) {
        if (this.toolbar) {
            while (this.toolbar.firstChild) {
                this.toolbar.removeChild(this.toolbar.firstChild);
            }
        } else {
            this.toolbar = document.createElement('blade-toolbar');
            if (this.content) {
                this.toolbar.classList.add('under-content');
            }
            this.element.appendChild(this.toolbar);
        }

        if (items.length) {
            this.toolbar.style.display = '';
        } else {
            this.toolbar.style.display = 'none';
        }

        items.forEach(item => {
            if (item instanceof Node) {
                this.toolbar.appendChild(item);
            } else if (item.element instanceof Node) {
                this.toolbar.appendChild(item.element);
            } else {
                console.error('Could not add item to Toolbar', item);
            }
        });
    }

    setContent(...items) {
        if (this.content) {
            while (this.content.firstChild) {
                this.content.removeChild(this.content.firstChild);
            }
        } else {
            this.content = document.createElement('blade-content');
            this.element.appendChild(this.content);
        }

        if (items.length) {
            this.content.style.display = '';
        } else {
            this.content.style.display = 'none';
        }

        items.forEach(item => {
            if (item instanceof Node) {
                this.content.appendChild(item);
            } else if (item.element instanceof Node) {
                this.content.appendChild(item.element);
            } else {
                console.error('Could not add item to Toolbar', item);
            }
        });
    }

    setCustomContent(...items) {
        if (this.customContent) {
            while (this.customContent.firstChild) {
                this.customContent.removeChild(this.customContent.firstChild);
            }
        } else {
            this.customContent = document.createElement('blade-custom-content');
            this.element.appendChild(this.customContent);
        }

        if (items.length) {
            this.customContent.style.display = '';
        } else {
            this.customContent.style.display = 'none';
        }

        items.forEach(item => {
            if (item instanceof Node) {
                this.customContent.appendChild(item);
            } else if (item.element instanceof Node) {
                this.customContent.appendChild(item.element);
            } else {
                console.error('Could not add item to Toolbar', item);
            }
        });
    }

    open(data) {

    }

    onClose(callback) {
        this.onCloseCallback = callback;

        return this;
    }

    close(data) {
        if (this.onCloseCallback) {
            this.onCloseCallback(data);
        }
    }

    addCloseButton(app) {
        this.closeButton = document.createElement('blade-title-close');
        this.closeButton.setAttribute('tabindex', 0);
        this.closeButton.addEventListener('click', () => this.app.closeBladesAfter(this).closeBlade(this));
        if (this.title) {
            this.title.appendChild(this.closeButton);
        }
    }
}
