// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'reservation_insert_request.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ReservationInsertRequest _$ReservationInsertRequestFromJson(
        Map<String, dynamic> json) =>
    ReservationInsertRequest()
      ..arrival = json['arrival'] == null
          ? null
          : DateTime.parse(json['arrival'] as String)
      ..departure = json['departure'] == null
          ? null
          : DateTime.parse(json['departure'] as String)
      ..totalPrice = (json['totalPrice'] as num?)?.toDouble()
      ..estimatedArrivalTime = json['estimatedArrivalTime'] as String?
      ..userId = json['userId'] as int?
      ..roomId = json['roomId'] as int?
      ..paymentDetails = json['paymentDetails'] == null
          ? null
          : PaymetDetailsInsertRequest.fromJson(
              json['paymentDetails'] as Map<String, dynamic>)
      ..facilityIds = (json['facilityIds'] as List<dynamic>?)
          ?.map((e) => e as int)
          .toList();

Map<String, dynamic> _$ReservationInsertRequestToJson(
        ReservationInsertRequest instance) =>
    <String, dynamic>{
      'arrival': instance.arrival?.toIso8601String(),
      'departure': instance.departure?.toIso8601String(),
      'totalPrice': instance.totalPrice,
      'estimatedArrivalTime': instance.estimatedArrivalTime,
      'userId': instance.userId,
      'roomId': instance.roomId,
      'paymentDetails': instance.paymentDetails,
      'facilityIds': instance.facilityIds,
    };
