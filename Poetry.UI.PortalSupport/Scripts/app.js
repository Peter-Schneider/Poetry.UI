


/* APP */

class App {
    constructor() {
        this.name = this.constructor.name;
        this.bladeClasses = [];
        this.blades = [];
        this.root = document.createElement('app');
    }

    addBlade(blade) {
        this.bladeClasses.push(blade);
    }

    openBlade(name, data) {
        var bladeClass = this.bladeClasses.find(b => b.name == name);

        if (!bladeClass) {
            throw 'Blade not found';
        }

        var blade = new bladeClass(this);

        blade.open(data);

        this.blades.push(blade);
        this.root.appendChild(blade.root);

        blade.root.scrollIntoView({
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
        this.root.removeChild(blade.root);
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
            this.root.removeChild(b.root);
        });
        this.blades.splice(index + 1);

        return this;
    }
}
