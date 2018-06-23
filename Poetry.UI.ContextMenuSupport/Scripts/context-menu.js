


/* CONTEXT MENU */

class ContextMenu {
    constructor() {
        this.element = document.createElement('div');

        this.element.classList.add('context-menu-outer');

        var button = document.createElement('context-menu-button');

        button.tabindex = 0;

        button.addEventListener('click', () => {
            button.classList.add('active');
            this.menu.style.display = '';
            this.menu.classList.remove('context-menu-right');

            var rectangle = this.menu.getBoundingClientRect();
            var containerRectangle = this.element.offsetParent.getBoundingClientRect();

            if (rectangle.right > containerRectangle.right) {
                this.menu.classList.add('context-menu-right');
            }
        });

        document.addEventListener('click', event => {
            if (event.target == button || event.target == this.menu || event.target.offsetParent == this.menu) {
                return;
            }

            button.classList.remove('active');
            this.menu.style.display = 'none';
        });

        this.element.appendChild(button);

        this.menu = document.createElement('context-menu');
        this.menu.style.display = 'none';

        var arrowOuter = document.createElement('div');
        arrowOuter.classList.add('context-menu-arrow-outer');
        this.menu.appendChild(arrowOuter);

        var arrow1 = document.createElement('div');
        arrow1.classList.add('context-menu-arrow-1');
        arrowOuter.appendChild(arrow1);

        var arrow2 = document.createElement('div');
        arrow2.classList.add('context-menu-arrow-2');
        arrowOuter.appendChild(arrow2);

        this.element.appendChild(this.menu);
    }

    addItem(callback) {
        var item = new ContextMenuItem();

        callback(item);

        item.onClick(() => this.menu.style.display = 'none');

        item.appendTo(this.menu);

        return this;
    }
}

class ContextMenuItem {
    constructor() {
        this.element = document.createElement('context-menu-item');
    }

    setText(text) {
        this.element.innerText = text;

        return this;
    }

    onClick(callback) {
        this.element.addEventListener('click', callback);

        return this;
    }

    appendTo(element) {
        element.appendChild(this.element);

        return this;
    }
}

export default ContextMenu;