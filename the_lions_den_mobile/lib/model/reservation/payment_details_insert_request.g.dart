// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'payment_details_insert_request.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PaymetDetailsInsertRequest _$PaymetDetailsInsertRequestFromJson(
        Map<String, dynamic> json) =>
    PaymetDetailsInsertRequest()
      ..date =
          json['date'] == null ? null : DateTime.parse(json['date'] as String)
      ..paymentType = json['paymentType'] as String?
      ..currency = json['currency'] as String?
      ..stripeId = json['stripeId'] as String?;

Map<String, dynamic> _$PaymetDetailsInsertRequestToJson(
        PaymetDetailsInsertRequest instance) =>
    <String, dynamic>{
      'date': instance.date?.toIso8601String(),
      'paymentType': instance.paymentType,
      'currency': instance.currency,
      'stripeId': instance.stripeId,
    };
