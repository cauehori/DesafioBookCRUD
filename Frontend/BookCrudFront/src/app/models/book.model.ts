export interface BookModel {
    id: number;
    title: string;
    author: string;
    category?: string;
    totalPages:number;
    isActive: boolean;
}
