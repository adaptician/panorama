import {Pipe, PipeTransform} from "@angular/core";

@Pipe({
    name: 'isBusy',
})
export class IsBusyPipe implements PipeTransform {
    /**
     * @param dictionary The dictionary of busy indicators.
     * @param key The specific key to lookup.
     * @param timeStamp Updated by the calling component to trigger change detection.
     */
    transform(dictionary: { [key: string] : boolean }, key: string, timeStamp = undefined): boolean {
        if (!dictionary) {
            return false;
        }

        return dictionary[key];
    }
}