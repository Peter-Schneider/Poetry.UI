


/* APP */

class App {
    constructor(name) {
        this.name = name;
        this.bladeClasses = [];
        this.blades = [];
        this.element = document.createElement('app');
    }

    addBlade(blade) {
        this.bladeClasses.push(blade);
    }

    openBlade(name, ...parameters) {
        var bladeClass = this.bladeClasses.find(b => b.name == name);

        if (!bladeClass) {
            throw 'Blade not found';
        }

        var blade = new bladeClass(this);

        blade.app = this;

        blade.open(...parameters);

        this.blades.push(blade);
        this.element.appendChild(blade.element);

        blade.element.scrollIntoView({
            behavior: 'smooth'
        });

        return blade;
    }

    openStartBlade() {
        this.openBlade(this.bladeClasses[0].name);
    }

    closeBlade(blade, data) {
        if (!blade) {
            throw 'No blade specified';
        }

        var index = this.blades.indexOf(blade);

        if (index == -1) {
            throw 'Blade not found';
        }

        this.closeBladesAfter(blade);

        blade.close(data);
        this.element.removeChild(blade.element);
        this.blades.splice(index, 1);

        return this;
    }

    closeBladesAfter(blade) {
        if (!blade) {
            throw 'No blade specified';
        }

        var index = this.blades.indexOf(blade);

        if (index == -1) {
            throw 'Blade not found';
        }

        this.blades.slice(index + 1).reverse().forEach(b => {
            b.close();
            this.element.removeChild(b.element);
        });
        this.blades.splice(index + 1);

        return this;
    }
}
