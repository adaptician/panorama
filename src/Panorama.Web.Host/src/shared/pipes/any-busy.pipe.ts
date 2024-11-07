import {Pipe, PipeTransform} from "@angular/core";

@Pipe({
    name: 'anyBusy',
})
export class AreAnyBusyPipe implements PipeTransform {
    /**
     * @param dictionary The dictionary of busy indicators.
     * @param timeStamp Updated by the calling component to trigger change detection.
     */
    transform(dictionary: { [key: string] : boolean }, timeStamp = undefined): boolean {
        if (!dictionary) {
            return false;
        }

        return Object.values(dictionary).some(isBusy => isBusy);
    }
}