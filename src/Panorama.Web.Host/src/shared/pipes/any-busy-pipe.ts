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

        for (const _key in dictionary) {
            const isBusy = dictionary[_key];
            if(isBusy) {
                // Return on first indicator that is busy.
                return true;
            }
        }

        return false;
    }
}