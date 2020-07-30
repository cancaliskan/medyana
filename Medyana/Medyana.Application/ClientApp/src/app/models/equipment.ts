export interface Result {
    id: number;
    name: string;
    provideDate?: any;
    quantity: number;
    unitPrice: number;
    usageRate: number;
    clinicId: number;
    clinic?: any;
    createdDate: Date;
    updateDate?: Date;
    deletedDate?: Date;
    isActive: boolean;
}

export interface Equipment {
    result: Result[];
    isSucceed: boolean;
    errorMessage?: any;
}
