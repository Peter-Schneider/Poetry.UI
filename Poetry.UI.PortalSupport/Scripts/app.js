import SlideEffect from './Effects/slide.js';
import FadeInEffect from './Effects/fade-in.js';
import FadeOutEffect from './Effects/fade-out.js';



/* APP */

class App {
    constructor(name) {
        this.name = name;
        this.blades = [];
        this.element = document.createElement('app');
    }

    open() {

    }

    openBlade(blade, parentBlade) {
        var done;
        var promise = new Promise(resolve => done = resolve);

        var open = () => {
            this.blades.push(blade);
            this.element.appendChild(blade.element);

            blade.element.scrollIntoView({
                behavior: 'smooth'
            });

            blade.element.style.overflow = 'hidden';

            new FadeInEffect(blade.inner).onComplete(() => {
                blade.element.style.overflow = '';

                blade.element.focus();

                done();
            });
        }

        var bladesAfterParentBlade = parentBlade ? this.blades.slice(this.blades.indexOf(parentBlade) + 1) : [];

        if (bladesAfterParentBlade.length) {
            return this.closeBlade(bladesAfterParentBlade[0]).then(open);
        } else {
            return open();
        }

        return promise;
    }

    closeBlade(blade, ...parameters) {
        if (!blade) {
            throw 'No blade specified';
        }

        var index = this.blades.indexOf(blade);

        if (index == -1) {
            throw 'Blade not found';
        }

        var done;
        var promise = new Promise(resolve => done = resolve);

        this.blades.slice(index).reverse().forEach((b, i, array) => {
            b.element.style.overflow = 'hidden';
            new FadeOutEffect(b.inner, i * 200).onComplete(() => {
                b.element.removeChild(b.inner);

                new SlideEffect(b.element, '25rem', '0').onComplete(() => {
                    this.element.removeChild(b.element);
                    this.blades.splice(this.blades.indexOf(b), 1);

                    if (i == array.length - 1) {
                        b.triggerOnClose(...parameters);
                        done();
                    } else {
                        b.triggerOnClose();
                    }
                });
            });
        });

        return promise;
    }
}

export default App;