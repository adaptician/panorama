import {Pipe, PipeTransform} from "@angular/core";

interface IIncludeUserId {
    userId: number;
}

@Pipe({
    name: 'containsUser',
})
export class ContainsUserPipe implements PipeTransform {
    /**
     * @param userList
     * @param userId
     */
    transform(userList: IIncludeUserId[], userId: number): boolean {
        if (!userList || userList.length <= 0) return false;

        return userList.some(x => x.userId === userId);
    }
}